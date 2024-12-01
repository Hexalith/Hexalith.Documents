namespace Hexalith.Documents.UI.Components.Documents.Services;

using System.Threading.Tasks;

/// <summary>
/// Service for uploading documents.
/// </summary>
public class DocumentUploadService : IDocumentUploadService
{
    /// <inheritdoc/>
    public Task UploadDocumentAsync(string documentGlobalId, Stream uploadStream) => Task.CompletedTask;
}