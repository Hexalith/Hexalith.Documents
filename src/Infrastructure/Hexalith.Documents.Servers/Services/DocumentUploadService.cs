namespace Hexalith.Documents.Servers.Services;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Hexalith.Documents.Application;

/// <summary>
/// Service for uploading documents.
/// </summary>
public class DocumentUploadService : IDocumentUploadService
{
    /// <summary>
    /// Uploads a document asynchronously.
    /// </summary>
    /// <param name="documentContainerId">The document container identifier.</param>
    /// <param name="documentTypeId">The document type identifier.</param>
    /// <param name="documentId">The document identifier.</param>
    /// <param name="fileTypeId">The file type identifier.</param>
    /// <param name="fileId">The file identifier.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="tags">The tags associated with the document.</param>
    /// <param name="fileContent">The file content stream.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task UploadDocumentAsync(string documentContainerId, string documentTypeId, string documentId, string fileTypeId, string fileId, string fileName, IDictionary<string, string> tags, Stream fileContent) => throw new NotImplementedException();
}