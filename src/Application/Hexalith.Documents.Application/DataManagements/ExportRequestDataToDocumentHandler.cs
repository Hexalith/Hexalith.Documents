namespace Hexalith.Documents.Application.DataManagements;

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
    private readonly IUserDataService _userDataService;
    private readonly IWritableFileProvider _writableFileProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportRequestDataToDocumentHandler"/> class.
    /// </summary>
    /// <param name="userDataService">The user data service.</param>
    /// <param name="requestProcessor">The request processor.</param>
    /// <param name="commandProcessor">The command processor.</param>
    /// <param name="writableFileProvider">The writable file provider.</param>
    /// <param name="timeProvider">The time provider.</param>
    /// <param name="logger">The logger.</param>
    public ExportRequestDataToDocumentHandler(
        IUserDataService userDataService,
        IRequestProcessor requestProcessor,
        IDomainCommandProcessor commandProcessor,
        IWritableFileProvider writableFileProvider,
        TimeProvider timeProvider,
        ILogger<ExportRequestDataToDocumentHandler> logger)
        : base(timeProvider, logger)
    {
        ArgumentNullException.ThrowIfNull(requestProcessor);
        ArgumentNullException.ThrowIfNull(userDataService);
        ArgumentNullException.ThrowIfNull(commandProcessor);
        _userDataService = userDataService;
        _requestProcessor = requestProcessor;
        _commandProcessor = commandProcessor;
        _writableFileProvider = writableFileProvider;
    }

    /// <inheritdoc/>
    public override async Task<ExecuteCommandResult> DoAsync(ExportRequestDataToDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentContainerDetailsViewModel container = await GetUserContainerAsync(metadata, cancellationToken).ConfigureAwait(false);
        AddDocument addDocument = new(
            command.Id,
            container.Id,
            command.Id,
            null,
            null,
            metadata.Context.UserId,
            Time.GetLocalNow(),
            "Export");
        await _commandProcessor.SubmitAsync(addDocument, Metadata.CreateNew(addDocument, metadata, Time.GetLocalNow()), cancellationToken).ConfigureAwait(false);
        GetDocumentStorage getDocumentStorage = new(container.DocumentStorageId);
        DocumentStorage? documentPartition = (await _requestProcessor.ProcessAsync(
                getDocumentStorage,
                Metadata.CreateNew(getDocumentStorage, metadata, Time.GetLocalNow()),
                cancellationToken)
            .ConfigureAwait(false) as GetDocumentStorage)?.Result;
        DataExportStarted exportStarted = new(command.Id, Time.GetLocalNow());
        aggregate = new DataManagement(exportStarted);
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

    private async Task<DocumentContainerDetailsViewModel> GetUserContainerAsync(Metadata metadata, CancellationToken cancellationToken)
    {
        GetDocumentContainerDetails? getDocumentContainer = new(metadata.Context.UserId);
        Metadata meta = Metadata.CreateNew(getDocumentContainer, metadata, Time.GetLocalNow());
        getDocumentContainer = await _requestProcessor
            .ProcessAsync(getDocumentContainer, meta, cancellationToken)
            .ConfigureAwait(false) as GetDocumentContainerDetails;
        if (getDocumentContainer?.Result is not DocumentContainerDetailsViewModel container)
        {
            // Create the user default container
            CreateDocumentContainer createDocumentContainer = new(
                metadata.Context.UserId,
                "Default",
                metadata.Context.UserId + " user data",
                "Users",
                "The user default document container",
                null);
            await _commandProcessor.SubmitAsync(createDocumentContainer, Metadata.CreateNew(createDocumentContainer, metadata, Time.GetLocalNow()), cancellationToken).ConfigureAwait(false);
            throw new InvalidOperationException("User document container not found. Created the default container. Retry the export.");
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
        file.Stream.Write(Encoding.UTF8.GetBytes("[\n"));
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