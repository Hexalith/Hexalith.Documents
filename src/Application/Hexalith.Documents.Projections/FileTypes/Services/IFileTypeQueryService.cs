namespace Hexalith.Documents.Projections.FileTypes.Services;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Hexalith.Application.Services;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Defines the contract for a service that provides query operations for file types.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IIdDescriptionService"/> and provides methods
/// for retrieving and searching file type information asynchronously.
/// The service supports operations such as getting detailed information about a specific file type,
/// retrieving paginated lists of file type summaries, and searching file types based on text criteria.
/// </remarks>
public interface IFileTypeQueryService : IIdDescriptionService
{
    /// <summary>
    /// Retrieves the detailed information for a file type with the specified ID.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="id">The unique identifier of the file type.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the file type details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the file type with the specified ID is not found.</exception>
    Task<FileTypeDetailsViewModel> GetDetailsAsync(ClaimsPrincipal user, string id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves summaries for all file types in the system.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of file type summaries.</returns>
    /// <remarks>
    /// This method is equivalent to calling <see cref="GetSummariesAsync(int, int)"/> with skip = 0 and count = 0,
    /// which retrieves all file types without pagination.
    /// </remarks>
    Task<IEnumerable<FileTypeSummaryViewModel>> GetSummariesAsync(ClaimsPrincipal user, CancellationToken cancellationToken)
        => GetSummariesAsync(user, 0, 0, cancellationToken);

    /// <summary>
    /// Retrieves a paginated list of file type summaries.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="skip">The number of file types to skip in the result set. Must be non-negative.</param>
    /// <param name="take">The maximum number of file types to return. If 0, returns all file types.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of file type summaries.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="skip"/> is negative.</exception>
    /// <remarks>
    /// This method supports pagination by allowing the caller to specify how many items to skip and how many to return.
    /// When count is 0, the method returns all file types starting from the skip position.
    /// </remarks>
    Task<IEnumerable<FileTypeSummaryViewModel>> GetSummariesAsync(ClaimsPrincipal user, int skip, int take, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for file types based on the provided search text.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="searchText">The text to search for in file type information.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of matching file type summaries.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="searchText"/> is null or empty.</exception>
    /// <remarks>
    /// The search is performed on relevant fields of the file type (such as name, description, or other searchable attributes).
    /// The exact search algorithm and matched fields depend on the implementation.
    /// The search is typically case-insensitive and may support partial matches.
    /// </remarks>
    Task<IEnumerable<FileTypeSummaryViewModel>> SearchSummariesAsync(ClaimsPrincipal user, string searchText, CancellationToken cancellationToken);
}