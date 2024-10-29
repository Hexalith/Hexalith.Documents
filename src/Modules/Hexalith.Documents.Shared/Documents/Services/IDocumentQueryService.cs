namespace Hexalith.Documents.Shared.Documents.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Hexalith.Documents.Shared.Documents.ViewModels;
using Hexalith.UI.Components.Services;

/// <summary>
/// Defines the contract for a service that provides query operations for documents.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IIdDescriptionService"/> and provides methods
/// for retrieving and searching document information asynchronously.
/// </remarks>
public interface IDocumentQueryService : IIdDescriptionService
{
    /// <summary>
    /// Retrieves the detailed information for a document with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier of the document.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the document with the specified ID is not found.</exception>
    Task<DocumentDetails> GetDetailsAsync(string id);

    /// <summary>
    /// Retrieves summaries for all documents.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of document summaries.</returns>
    /// <remarks>
    /// This method is equivalent to calling <see cref="GetSummariesAsync(int, int)"/> with skip = 0 and count = 0.
    /// </remarks>
    Task<IEnumerable<DocumentSummary>> GetSummariesAsync()
        => GetSummariesAsync(0, 0);

    /// <summary>
    /// Retrieves a paginated list of document summaries.
    /// </summary>
    /// <param name="skip">The number of documents to skip.</param>
    /// <param name="count">The maximum number of documents to return. If 0, returns all documents.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of document summaries.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="skip"/> is negative.</exception>
    Task<IEnumerable<DocumentSummary>> GetSummariesAsync(int skip, int count);

    /// <summary>
    /// Searches for documents based on the provided search text.
    /// </summary>
    /// <param name="searchText">The text to search for in document information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of matching document summaries.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="searchText"/> is null or empty.</exception>
    /// <remarks>
    /// The search is typically performed on fields such as name, email, or phone number.
    /// The exact fields and search algorithm may vary based on the implementation.
    /// </remarks>
    Task<IEnumerable<DocumentSummary>> SearchSummariesAsync(string searchText);
}
