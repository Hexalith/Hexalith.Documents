﻿namespace Hexalith.Documents.Servers.Services;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Services;
using Hexalith.Application.Sessions.Models;
using Hexalith.Application.Sessions.Services;
using Hexalith.Documents.Application;
using Hexalith.Documents.Application.Services;
using Hexalith.Documents;
using Hexalith.Documents.DocumentContainers;
using Hexalith.Documents.DocumentStorages;
using Hexalith.Documents.DocumentTypes;
using Hexalith.Documents.FileTypes;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domain.Events;

using Microsoft.Extensions.Logging;

/// <summary>
/// Service for uploading documents.
/// </summary>
public partial class DocumentUploadService : IDocumentUploadService
{
    private readonly IAggregateService _aggregateService;
    private readonly ILogger<DocumentUploadService> _logger;
    private readonly ISessionService _sessionService;
    private readonly IWritableFileProvider _writableFileProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentUploadService"/> class.
    /// </summary>
    /// <param name="sessionService">The session service.</param>
    /// <param name="aggregateService">The aggregate service.</param>
    /// <param name="writableFileProvider">The writable file provider.</param>
    /// <param name="logger">The logger.</param>
    public DocumentUploadService(
        [NotNull] ISessionService sessionService,
        [NotNull] IAggregateService aggregateService,
        [NotNull] IWritableFileProvider writableFileProvider,
        [NotNull] ILogger<DocumentUploadService> logger)
    {
        ArgumentNullException.ThrowIfNull(sessionService);
        ArgumentNullException.ThrowIfNull(aggregateService);
        ArgumentNullException.ThrowIfNull(writableFileProvider);
        ArgumentNullException.ThrowIfNull(logger);
        _sessionService = sessionService;
        _aggregateService = aggregateService;
        _writableFileProvider = writableFileProvider;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task UploadDocumentAsync(
        string correlationId,
        string userId,
        string documentContainerId,
        string documentId,
        string documentTypeId,
        string fileTypeId,
        string fileName,
        IEnumerable<DocumentTag> tags,
        Stream fileContent,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);
        ArgumentException.ThrowIfNullOrWhiteSpace(documentContainerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(documentId);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentNullException.ThrowIfNull(tags);
        ArgumentNullException.ThrowIfNull(fileContent);
        cancellationToken.ThrowIfCancellationRequested();
        SessionInformation session = await _sessionService
            .GetAsync(userId, cancellationToken)
            .ConfigureAwait(false);
        Task<DocumentType> documentTypeTask = GetDocumentTypeAsync(documentTypeId, session.PartitionId, cancellationToken);
        Task<FileType> fileTypeTask = GetFileTypeAsync(fileTypeId, session.PartitionId, cancellationToken);
        DocumentContainer container = await GetContainerAsync(documentContainerId, session.PartitionId, cancellationToken).ConfigureAwait(false);
        DocumentStorage storage = await GetStorageAsync(container.DocumentStorageId, session.PartitionId, cancellationToken).ConfigureAwait(false);
        string path = Path.Combine(container.Path, documentId);
        FileType fileType = await fileTypeTask.ConfigureAwait(false);
        DocumentType documentType = await documentTypeTask.ConfigureAwait(false);
        IEnumerable<DocumentTag> fileTags = [
            ..tags,
            ..container.Tags,
            ..documentType.Tags,
            new("UserId", userId, true),
            new("CorrelationId", correlationId, true),
            new("SessionId", session.SessionId, true),
            new("PartitionId", session.PartitionId, true),
            new("DocumentId", documentId, true),
            new("FileName", fileName, true),
            new("FilePath", path, true),
            new("FileContentType", fileType.ContentType, true),
            new("DocumentContainerId", documentContainerId, true),
            new("DocumentStorageId", container.DocumentStorageId, true)];
#pragma warning disable CA2007 // Consider calling ConfigureAwait on the awaited task
        await using IWritableFile file = await _writableFileProvider
            .CreateFileAsync(
                storage.StorageType,
                storage.ConnectionString,
                path,
                fileName,
                fileTags.Select(t => (t.Key, t.Value)),
                cancellationToken).ConfigureAwait(false);
#pragma warning restore CA2007 // Consider calling ConfigureAwait on the awaited task
        await fileContent.CopyToAsync(file.Stream, cancellationToken).ConfigureAwait(false);
        _ = await file.CloseAsync(cancellationToken).ConfigureAwait(false);
        LogFileUploadedInformation(_logger, userId, documentId, fileName, path, session.PartitionId);
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "File '{FileName}' uploaded at '{FilePath}' by '{UserId}' for document '{DocumentId}' in partition '{PartitionId}'.")]
    private static partial void LogFileUploadedInformation(ILogger logger, string userId, string documentId, string fileName, string filePath, string partitionId);

    private async Task<DocumentContainer> GetContainerAsync(string documentContainerId, string partitionId, CancellationToken cancellationToken)
    {
        string globalId = Metadata.CreateAggregateGlobalId(
            partitionId,
            DocumentDomainHelper.DocumentContainerAggregateName,
            documentContainerId);
        SnapshotEvent? snapshot = await _aggregateService
            .GetSnapshotAsync(DocumentDomainHelper.DocumentContainerAggregateName, globalId, cancellationToken)
            .ConfigureAwait(false) ?? throw new InvalidOperationException($"Document container '{documentContainerId}' not found.");
        DocumentContainer aggregate = snapshot.GetAggregate<DocumentContainer>();
        return aggregate;
    }

    private async Task<DocumentType> GetDocumentTypeAsync(string documentTypeId, string partitionId, CancellationToken cancellationToken)
    {
        string globalId = Metadata.CreateAggregateGlobalId(
            partitionId,
            DocumentDomainHelper.DocumentTypeAggregateName,
            documentTypeId);
        SnapshotEvent? snapshot = await _aggregateService
            .GetSnapshotAsync(DocumentDomainHelper.DocumentTypeAggregateName, globalId, cancellationToken)
            .ConfigureAwait(false) ?? throw new InvalidOperationException($"Document type '{documentTypeId}' not found.");
        DocumentType aggregate = snapshot.GetAggregate<DocumentType>();
        return aggregate;
    }

    private async Task<FileType> GetFileTypeAsync(string documentTypeId, string partitionId, CancellationToken cancellationToken)
    {
        string globalId = Metadata.CreateAggregateGlobalId(
            partitionId,
            DocumentDomainHelper.FileTypeAggregateName,
            documentTypeId);
        SnapshotEvent? snapshot = await _aggregateService
            .GetSnapshotAsync(DocumentDomainHelper.FileTypeAggregateName, globalId, cancellationToken)
            .ConfigureAwait(false) ?? throw new InvalidOperationException($"File type '{documentTypeId}' not found.");
        FileType aggregate = snapshot.GetAggregate<FileType>();
        return aggregate;
    }

    private async Task<DocumentStorage> GetStorageAsync(string documentStorageId, string partitionId, CancellationToken cancellationToken)
    {
        string globalId = Metadata.CreateAggregateGlobalId(
            partitionId,
            DocumentDomainHelper.DocumentStorageAggregateName,
            documentStorageId);
        SnapshotEvent? snapshot = await _aggregateService
            .GetSnapshotAsync(DocumentDomainHelper.DocumentStorageAggregateName, globalId, cancellationToken)
            .ConfigureAwait(false) ?? throw new InvalidOperationException($"Document storage '{documentStorageId}' not found.");
        DocumentStorage aggregate = snapshot.GetAggregate<DocumentStorage>();
        return aggregate;
    }
}