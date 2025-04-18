// <copyright file="IDocumentUploadService.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Application;

using System.Threading.Tasks;

using Hexalith.Documents.ValueObjects;

/// <summary>
/// Interface for document upload service.
/// </summary>
public interface IDocumentUploadService
{
    /// <summary>
    /// Uploads a document asynchronously.
    /// </summary>
    /// <param name="correlationId">The correlation identifier.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="documentContainerId">The document container identifier.</param>
    /// <param name="documentId">The document identifier.</param>
    /// <param name="documentTypeId">The document type identifier.</param>
    /// <param name="fileTypeId">The file type identifier.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="tags">The document tags.</param>
    /// <param name="fileContent">Content of the file.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UploadDocumentAsync(
        string correlationId,
        string userId,
        string documentContainerId,
        string documentId,
        string documentTypeId,
        string fileTypeId,
        string fileName,
        IEnumerable<DocumentTag> tags,
        Stream fileContent,
        CancellationToken cancellationToken);
}