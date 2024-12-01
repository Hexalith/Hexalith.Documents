namespace Hexalith.Documents.UI.Components.Documents.Services;

using System.Threading.Tasks;

/// <summary>
/// Defines the contract for a service that handles document uploads.
/// </summary>
public interface IDocumentUploadService
{
    /// <summary>
    /// Uploads a document asynchronously.
    /// </summary>
    /// <param name="documentGlobalId">The global identifier of the document.</param>
    /// <param name="uploadStream">The stream containing the document data.</param>
    /// <returns>A task that represents the asynchronous upload operation.</returns>
    Task UploadDocumentAsync(string documentGlobalId, Stream uploadStream);
}