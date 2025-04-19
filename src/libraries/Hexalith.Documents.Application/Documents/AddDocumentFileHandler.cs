// <copyright file="AddDocumentFileHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Application.Documents;

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
using Hexalith.Documents.DataManagements;
using Hexalith.Documents.DocumentStorages;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains;
using Hexalith.Extensions.Helpers;
using Hexalith.PolymorphicSerializations;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the export request data to document command.
/// </summary>
public class AddDocumentFileHandler : DomainCommandHandler<ExportRequestDataToDocument>
{
    private readonly IDomainCommandProcessor _commandProcessor;
    private readonly IRequestProcessor _requestProcessor;
    private readonly IWritableFileProvider _writableFileProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddDocumentFileHandler"/> class.
    /// </summary>
    /// <param name="requestProcessor">The request processor.</param>
    /// <param name="commandProcessor">The command processor.</param>
    /// <param name="writableFileProvider">The writable file provider.</param>
    /// <param name="timeProvider">The time provider.</param>
    /// <param name="logger">The logger.</param>
    public AddDocumentFileHandler(
        IRequestProcessor requestProcessor,
        IDomainCommandProcessor commandProcessor,
        IWritableFileProvider writableFileProvider,
        TimeProvider timeProvider,
        ILogger<AddDocumentFileHandler> logger)
        : base(timeProvider, logger)
    {
        ArgumentNullException.ThrowIfNull(requestProcessor);
        ArgumentNullException.ThrowIfNull(commandProcessor);
        _requestProcessor = requestProcessor;
        _commandProcessor = commandProcessor;
        _writableFileProvider = writableFileProvider;
    }

    /// <inheritdoc/>
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
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
            await using IWritableFile file = await _writableFileProvider.CreateFileAsync(
                documentPartition.StorageType,
                documentPartition.ConnectionString,
                container.Path,
                fileName,
                [],
                cancellationToken);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
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
            await stream.WriteAsync(Encoding.UTF8.GetBytes("{}"), cancellationToken);
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
    private static async Task WritePolymorphicObjectAsync(Stream stream, Polymorphic data, CancellationToken cancellationToken)
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
            Type type = firstItem is Polymorphic ? typeof(IEnumerable<Polymorphic>) : typeof(IEnumerable<>).MakeGenericType(firstItem.GetType());

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
        if (result is Polymorphic polymorphic)
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
        var meta = Metadata.CreateNew(getDocumentContainer, metadata, Time.GetLocalNow());
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
        await file.Stream.WriteAsync(Encoding.UTF8.GetBytes("[\n"), cancellationToken);
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
                    await file.Stream.WriteAsync(Encoding.UTF8.GetBytes(",\n"), cancellationToken);
                }

                await WriteRequestResultAsync(file.Stream, result, cancellationToken).ConfigureAwait(false);
            }

            request = request.HasNextChunk ? request.CreateNextChunkRequest() : null;
        }
        while (request is not null);
        await file.Stream.WriteAsync(Encoding.UTF8.GetBytes("\n]"), cancellationToken);
    }
}