namespace Hexalith.Documents.Projections.Documents.Services;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Hexalith.Application.Services;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Defines the contract for a service that provides query operations for documents.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IIdDescriptionService"/> and provides methods
/// for retrieving and searching document information asynchronously.
/// The service supports operations such as getting detailed information about a specific document,
/// retrieving paginated lists of document summaries, and searching documents based on text criteria.
/// </remarks>
public interface IDocumentQueryService : IIdDescriptionService
{
    /// <summary>
    /// Retrieves the detailed information for a document with the specified ID.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="id">The unique identifier of the document.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the document with the specified ID is not found.</exception>
    Task<DocumentDetailsViewModel> GetDetailsAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves summaries for all documents in the system.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of document summaries.</returns>
    /// <remarks>
    /// This method is equivalent to calling <see cref="GetSummariesAsync(int, int)"/> with skip = 0 and count = 0,
    /// which retrieves all documents without pagination.
    /// </remarks>
    Task<IEnumerable<DocumentSummaryViewModel>> GetSummariesAsync(ClaimsPrincipal user, CancellationToken cancellationToken)
        => GetSummariesAsync(user, 0, 0, cancellationToken);

    /// <summary>
    /// Retrieves a paginated list of document summaries.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="skip">The number of documents to skip in the result set. Must be non-negative.</param>
    /// <param name="take">The maximum number of documents to return. If 0, returns all documents.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of document summaries.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="skip"/> is negative.</exception>
    /// <remarks>
    /// This method supports pagination by allowing the caller to specify how many items to skip and how many to return.
    /// When count is 0, the method returns all documents starting from the skip position.
    /// </remarks>
    Task<IEnumerable<DocumentSummaryViewModel>> GetSummariesAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for documents based on the provided search text.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="searchText">The text to search for in document information.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of matching document summaries.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="searchText"/> is null or empty.</exception>
    /// <remarks>
    /// The search is performed on relevant fields of the document (such as name, description, or other searchable attributes).
    /// The exact search algorithm and matched fields depend on the implementation.
    /// The search is typically case-insensitive and may support partial matches.
    /// </remarks>
    Task<IEnumerable<DocumentSummaryViewModel>> SearchSummariesAsync(ClaimsPrincipal user, string searchText, CancellationToken cancellationToken);
}