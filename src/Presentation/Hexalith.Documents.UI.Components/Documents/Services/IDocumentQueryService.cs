namespace Hexalith.Documents.UI.Components.Documents.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Hexalith.Documents.UI.Components.Documents.ViewModels;
using Hexalith.UI.Components.Services;

/// <summary>
/// Defines the contract for a service that provides query operations for documents.
/// This service handles document retrieval, searching, and pagination functionality.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IIdDescriptionService"/> and provides methods
/// for retrieving and searching document information asynchronously.
/// Implementations should handle caching, data validation, and error handling appropriately.
/// </remarks>
public interface IDocumentQueryService : IIdDescriptionService
{
    /// <summary>
    /// Retrieves the detailed information for a document with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier of the document. Must not be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document details as a <see cref="DocumentDetails"/> object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the document with the specified ID is not found in the system.</exception>
    /// <remarks>
    /// This method performs a detailed lookup of the document, including all associated metadata and content.
    /// The operation may involve database queries or external service calls.
    /// </remarks>
    Task<DocumentDetails> GetDetailsAsync(string id);

    /// <summary>
    /// Retrieves summaries for all documents in the system.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="DocumentSummary"/> objects.</returns>
    /// <remarks>
    /// This method is a convenience wrapper that calls <see cref="GetSummariesAsync(int, int)"/> with skip = 0 and count = 0,
    /// effectively retrieving all documents without pagination. Use with caution on large datasets.
    /// </remarks>
    Task<IEnumerable<DocumentSummary>> GetSummariesAsync()
        => GetSummariesAsync(0, 0);

    /// <summary>
    /// Retrieves a paginated list of document summaries.
    /// </summary>
    /// <param name="skip">The number of documents to skip. Must be non-negative.</param>
    /// <param name="count">The maximum number of documents to return. If 0, returns all documents after the skip point.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="DocumentSummary"/> objects.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="skip"/> is negative.</exception>
    /// <remarks>
    /// This method supports pagination for efficient data retrieval in user interfaces.
    /// The documents are typically returned in a predetermined order (e.g., by creation date or ID).
    /// </remarks>
    Task<IEnumerable<DocumentSummary>> GetSummariesAsync(int skip, int count);

    /// <summary>
    /// Searches for documents based on the provided search text.
    /// </summary>
    /// <param name="searchText">The text to search for in document information. Must not be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of matching <see cref="DocumentSummary"/> objects.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="searchText"/> is null or empty.</exception>
    /// <remarks>
    /// The search is typically performed on multiple fields such as document title, content, metadata, or tags.
    /// The exact search algorithm and matched fields depend on the implementation.
    /// The search may be case-insensitive and might support partial matches or advanced search syntax.
    /// Results are typically returned in order of relevance to the search terms.
    /// </remarks>
    Task<IEnumerable<DocumentSummary>> SearchSummariesAsync(string searchText);
}