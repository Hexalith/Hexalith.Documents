namespace Hexalith.Documents.Application.DataManagements;

using System.Diagnostics.CodeAnalysis;
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
using Hexalith.Documents.Commands.DataManagements;
using Hexalith.Documents.Commands.DocumentContainers;
using Hexalith.Documents.Commands.Documents;
using Hexalith.Documents.Domain.DataManagements;
using Hexalith.Documents.Domain.DocumentStorages;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Domain.Aggregates;
using Hexalith.Extensions.Helpers;
using Hexalith.PolymorphicSerialization;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the export request data to document command.
/// </summary>
public class ExportRequestDataToDocumentHandler : DomainCommandHandler<ExportRequestDataToDocument>
{
    private readonly IDomainCommandProcessor _commandProcessor;
    private readonly IRequestProcessor _requestProcessor;
    private readonly IWritableFileProvider _writableFileProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportRequestDataToDocumentHandler"/> class.
    /// </summary>
    /// <param name="requestProcessor">The request processor.</param>
    /// <param name="commandProcessor">The command processor.</param>
    /// <param name="writableFileProvider">The writable file provider.</param>
    /// <param name="timeProvider">The time provider.</param>
    /// <param name="logger">The logger.</param>
    public ExportRequestDataToDocumentHandler(
        IRequestProcessor requestProcessor,
        IDomainCommandProcessor commandProcessor,
        IWritableFileProvider writableFileProvider,
        TimeProvider timeProvider,
        ILogger<ExportRequestDataToDocumentHandler> logger)
        : base(timeProvider, logger)
    {
        ArgumentNullException.ThrowIfNull(requestProcessor);
        ArgumentNullException.ThrowIfNull(commandProcessor);
        _requestProcessor = requestProcessor;
        _commandProcessor = commandProcessor;
        _writableFileProvider = writableFileProvider;
    }

    /// <inheritdoc/>
    [SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "Async using not compatible")]
    [SuppressMessage("Minor Code Smell", "S2221:\"Exception\" should not be caught", Justification = "Need to capture all errors")]
    [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Need to capture all errors")]
    public override async Task<ExecuteCommandResult> DoAsync(ExportRequestDataToDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(metadata);
        DateTimeOffset now = Time.GetLocalNow();
        string fileName = $"{command.Id}.json";
        DocumentContainerDetailsViewModel container = await GetUserContainerAsync(metadata, cancellationToken).ConfigureAwait(false);
        GetDocumentStorage getDocumentStorage = new(container.DocumentStorageId);
        DocumentStorage? documentPartition = (await _requestProcessor.ProcessAsync(
                getDocumentStorage,
                Metadata.CreateNew(getDocumentStorage, metadata, now),
                cancellationToken)
            .ConfigureAwait(false) as GetDocumentStorage)?.Result;
        DataExportStarted exportStarted = new(command.Id, now);
        IDomainAggregate newAggregate = new DataManagement(exportStarted);
        if (documentPartition is null)
        {
            return new ExecuteCommandResult(
                newAggregate,
                [],
                [new DomainEventCancelled(
                "Document partition not found",
                new MessageState(exportStarted, metadata))],
                true);
        }

        try
        {
            await using IWritableFile file = await _writableFileProvider.CreateFileAsync(
                documentPartition.StorageType,
                documentPartition.ConnectionString,
                container.Path,
                fileName,
                [],
                cancellationToken);
            object? request = command.RequestObject;
            if (request is IChunkableRequest chunkedRequest && chunkedRequest.Take > 0)
            {
                await WriteRequestChunksResultAsync(file, chunkedRequest, metadata, cancellationToken).ConfigureAwait(false);
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
            newAggregate = newAggregate.Apply(exportCompleted).Aggregate;
            AddDocument addDocument = new(
                command.Id,
                command.Id,
                null,
                [new FileDescription(command.Id, FileContentType.Json.Id, fileName, fileName, size, "application/json")],
                metadata.Context.UserId,
                now,
                null,
                container.Id,
                "Export",
                []);
            await _commandProcessor.SubmitAsync(addDocument, Metadata.CreateNew(addDocument, metadata, now), cancellationToken).ConfigureAwait(false);
            return new ExecuteCommandResult(
                newAggregate,
                [exportStarted, exportCompleted],
                [exportStarted, exportCompleted],
                false);
        }
        catch (Exception ex)
        {
            return new ExecuteCommandResult(
                newAggregate,
                [],
                [new DomainEventCancelled(
                    ex.FullMessage(),
                    new MessageState(exportStarted, metadata))],
                true);
        }
    }

    private static string GetDocumentContainerId(string userId) => "User-" + userId;

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
            await stream.WriteAsync(Encoding.UTF8.GetBytes("{}"), cancellationToken).ConfigureAwait(false);
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
            type = firstItem is PolymorphicRecordBase ? typeof(IEnumerable<PolymorphicRecordBase>) : typeof(IEnumerable<>).MakeGenericType(firstItem.GetType());

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
                    [],
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

    private async Task<DocumentContainerDetailsViewModel> GetUserContainerAsync(Metadata metadata, CancellationToken cancellationToken)
    {
        string containerId = GetDocumentContainerId(metadata.Context.UserId);
        GetDocumentContainerDetails? getDocumentContainer = new(containerId);
        Metadata meta = Metadata.CreateNew(getDocumentContainer, metadata, Time.GetLocalNow());
        getDocumentContainer = await _requestProcessor
            .ProcessAsync(getDocumentContainer, meta, cancellationToken)
            .ConfigureAwait(false) as GetDocumentContainerDetails;
        if (getDocumentContainer?.Result is not DocumentContainerDetailsViewModel container)
        {
            // Create the user default container
            CreateDocumentContainer createDocumentContainer = new(
                containerId,
                "UserData",
                metadata.Context.UserId + " user data",
                metadata.Context.UserId,
                "The user default document container",
                null);
            await _commandProcessor.SubmitAsync(createDocumentContainer, Metadata.CreateNew(createDocumentContainer, metadata, Time.GetLocalNow()), cancellationToken).ConfigureAwait(false);
            throw new InvalidOperationException($"User document container {containerId} not found. Created the default container. Retry the export.");
        }

        return container;
    }

    /// <summary>
    /// Writes the request chunks result to the file asynchronously.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <param name="initialRequest">The initial request.</param>
    /// <param name="metadata">The metadata.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    private async Task WriteRequestChunksResultAsync(IWritableFile file, IChunkableRequest initialRequest, Metadata metadata, CancellationToken cancellationToken)
    {
        await file.Stream.WriteAsync(Encoding.UTF8.GetBytes("[\n"), cancellationToken).ConfigureAwait(false);
        IChunkableRequest? request = initialRequest;
        bool first = true;
        do
        {
            request = (IChunkableRequest)await _requestProcessor.ProcessAsync(
                    request,
                    Metadata.CreateNew(request, metadata, Time.GetLocalNow()),
                    cancellationToken).ConfigureAwait(false);
            foreach (object result in request.Results ?? [])
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    await file.Stream.WriteAsync(Encoding.UTF8.GetBytes(",\n"), cancellationToken).ConfigureAwait(false);
                }

                await WriteRequestResultAsync(file.Stream, result, cancellationToken).ConfigureAwait(false);
            }

            request = request.HasNextChunk ? request.CreateNextChunkRequest() : null;
        }
        while (request is not null);
        await file.Stream.WriteAsync(Encoding.UTF8.GetBytes("\n]"), cancellationToken).ConfigureAwait(false);
    }
}