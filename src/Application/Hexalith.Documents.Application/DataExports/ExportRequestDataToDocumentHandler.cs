namespace Hexalith.Documents.Application.DataExports;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Commands;
using Hexalith.Application.Events;
using Hexalith.Application.Metadatas;
using Hexalith.Application.Requests;
using Hexalith.Application.States;
using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Commands.DataExports;
using Hexalith.Documents.Domain.DataExports;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Domain.DocumentPartitions;
using Hexalith.Documents.Events.DataExports;
using Hexalith.Documents.Requests.DocumentPartitions;
using Hexalith.Domain.Aggregates;
using Hexalith.Extensions.Helpers;
using Hexalith.PolymorphicSerialization;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the export request data to document command.
/// </summary>
public class ExportRequestDataToDocumentHandler : DomainCommandHandler<ExportRequestDataToDocument>
{
    private readonly IRequestProcessor _requestProcessor;
    private readonly IUserDataService _userDataService;
    private readonly IWritableFileProvider _writableFileProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportRequestDataToDocumentHandler"/> class.
    /// </summary>
    /// <param name="userDataService">The user data service.</param>
    /// <param name="requestProcessor">The request processor.</param>
    /// <param name="writableFileProvider">The writable file provider.</param>
    /// <param name="timeProvider">The time provider.</param>
    /// <param name="logger">The logger.</param>
    public ExportRequestDataToDocumentHandler(
        IUserDataService userDataService,
        IRequestProcessor requestProcessor,
        IWritableFileProvider writableFileProvider,
        TimeProvider timeProvider,
        ILogger<ExportRequestDataToDocumentHandler> logger)
        : base(timeProvider, logger)
    {
        ArgumentNullException.ThrowIfNull(requestProcessor);
        ArgumentNullException.ThrowIfNull(userDataService);
        _userDataService = userDataService;
        _requestProcessor = requestProcessor;
        _writableFileProvider = writableFileProvider;
    }

    /// <inheritdoc/>
    public override async Task<ExecuteCommandResult> DoAsync(ExportRequestDataToDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(metadata);
        DocumentContainer container = await _userDataService.GetUserDocumentContainerGlobalIdAsync(metadata.Context.PartitionId, metadata.Context.UserId, cancellationToken).ConfigureAwait(false);
        GetDocumentPartition getDocumentPartition = new(container.DocumentPartitionId);
        DocumentPartition? documentPartition = (await _requestProcessor.ProcessAsync(getDocumentPartition, Metadata.CreateNew(getDocumentPartition, metadata, Time.GetLocalNow()), cancellationToken).ConfigureAwait(false)).Result;
        DataExportStarted exportStarted = new(command.Id, Time.GetLocalNow());
        aggregate = new DataExport(exportStarted);
        if (documentPartition is null)
        {
            return new ExecuteCommandResult(
                aggregate,
                [],
                [new DomainEventCancelled(
                "Document partition not found",
                new MessageState(exportStarted, metadata))],
                true);
        }
#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            using IWritableFile file = await _writableFileProvider.CreateFileAsync(
                documentPartition.StorageType,
                documentPartition.ConnectionString,
                container.Path,
                command.Id,
                cancellationToken).ConfigureAwait(false);
            object? request = command.RequestObject;
            if (request is IChunkableRequest chunkedRequest && chunkedRequest.Take > 0)
            {
                await WriteRequestChunkableResultAsync(file, chunkedRequest, metadata, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                request = await _requestProcessor.ProcessAsync(request, Metadata.CreateNew(request, metadata, Time.GetLocalNow()), cancellationToken).ConfigureAwait(false);
                if (request is ICollectionRequest collectionRequest)
                {
                    await WriteRequestCollectionResultAsync(file.Stream, collectionRequest.Results, cancellationToken).ConfigureAwait(false);
                }
                else if (request is IRequest resultRequest)
                {
                    await WriteRequestResultAsync(file.Stream, resultRequest.Result, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    await WriteRequestResultAsync(file.Stream, request, cancellationToken).ConfigureAwait(false);
                }
            }

            long size = await file.CloseAsync(cancellationToken).ConfigureAwait(false);
            DataExportCompleted exportCompleted = new(
                    command.Id,
                    size,
                    Time.GetLocalNow());
            aggregate = aggregate.Apply(exportCompleted).Aggregate;
            return new ExecuteCommandResult(
                aggregate,
                [exportStarted, exportCompleted],
                [exportStarted, exportCompleted],
                false);
        }
        catch (Exception ex)
        {
            return new ExecuteCommandResult(
                aggregate,
                [],
                [new DomainEventCancelled(
                    ex.FullMessage(),
                    new MessageState(exportStarted, metadata))],
                true);
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }

    /// <summary>
    /// Writes the object to the stream asynchronously.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="data">The data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private static async Task WriteObjectAsync(Stream stream, object? data, CancellationToken cancellationToken)
    {
        if (data is null)
        {
            stream.Write(Encoding.UTF8.GetBytes("{}"));
            return;
        }

        await JsonSerializer.SerializeAsync(
            stream,
            data,
            data.GetType(),
            JsonSerializerOptions.Web,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Writes the polymorphic object to the stream asynchronously.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="data">The data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private static async Task WritePolymorphicObjectAsync(Stream stream, PolymorphicRecordBase data, CancellationToken cancellationToken)
        => await JsonSerializer.SerializeAsync(stream, data, PolymorphicHelper.DefaultJsonSerializerOptions, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Writes the request collection result to the stream asynchronously.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="data">The data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private static async Task WriteRequestCollectionResultAsync(Stream stream, IEnumerable<object>? data, CancellationToken cancellationToken)
    {
        object? firstItem = data?.FirstOrDefault();
        if (firstItem is not null)
        {
            Type type;
            if (firstItem is PolymorphicRecordBase)
            {
                type = typeof(IEnumerable<PolymorphicRecordBase>);
            }
            else
            {
                type = typeof(IEnumerable<>).MakeGenericType(firstItem.GetType());
            }

            await JsonSerializer.SerializeAsync(
                    stream,
                    data,
                    type,
                    JsonSerializerOptions.Default,
                    cancellationToken).ConfigureAwait(false);
        }
        else
        {
            await JsonSerializer.SerializeAsync<IEnumerable<object>>(
                    stream,
                    Array.Empty<object>(),
                    JsonSerializerOptions.Default,
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Writes the request result to the stream asynchronously.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="result">The result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private static async Task WriteRequestResultAsync(Stream stream, object? result, CancellationToken cancellationToken)
    {
        if (result is PolymorphicRecordBase polymorphic)
        {
            await WritePolymorphicObjectAsync(stream, polymorphic, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            await WriteObjectAsync(stream, result, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Writes the request chunkable result to the file asynchronously.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <param name="initialRequest">The initial request.</param>
    /// <param name="metadata">The metadata.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private async Task WriteRequestChunkableResultAsync(IWritableFile file, IChunkableRequest initialRequest, Metadata metadata, CancellationToken cancellationToken)
    {
        file.Stream.Write(Encoding.UTF8.GetBytes("[\n"));
        IChunkableRequest? request = initialRequest;
        bool first = true;
        do
        {
            request = await _requestProcessor.ProcessAsync(
                    request,
                    Metadata.CreateNew(request, metadata, Time.GetLocalNow()),
                    cancellationToken).ConfigureAwait(false);
            foreach (object result in request.Results ?? Array.Empty<object>())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    file.Stream.Write(Encoding.UTF8.GetBytes(",\n"));
                }

                await WriteRequestResultAsync(file.Stream, result, cancellationToken).ConfigureAwait(false);
            }

            if (request.HasNextChunk)
            {
                request = request.CreateNextChunkRequest();
            }
            else
            {
                request = null;
            }
        }
        while (request is not null);
        file.Stream.Write(Encoding.UTF8.GetBytes("\n]"));
    }
}