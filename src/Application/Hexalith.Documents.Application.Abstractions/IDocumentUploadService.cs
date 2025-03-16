namespace Hexalith.Documents.Application;

using System.Threading.Tasks;

/// <summary>
/// Interface for document upload service.
/// </summary>
public interface IDocumentUploadService
{
    /// <summary>
    /// Uploads a document asynchronously.
    /// </summary>
    /// <param name="documentContainerId">The ID of the document container.</param>
    /// <param name="documentTypeId">The ID of the document type.</param>
    /// <param name="documentId">The ID of the document.</param>
    /// <param name="fileTypeId">The ID of the file type.</param>
    /// <param name="fileId">The ID of the file.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="tags">The tags associated with the document.</param>
    /// <param name="fileContent">The content of the file as a stream.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UploadDocumentAsync(
        string documentContainerId,
        string documentTypeId,
        string documentId,
        string fileTypeId,
        string fileId,
        string fileName,
        IDictionary<string, string> tags,
        Stream fileContent);
}