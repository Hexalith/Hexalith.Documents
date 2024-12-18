﻿namespace Hexalith.Documents.UI.Services.DocumentTypes.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Hexalith.Application.Services;
using Hexalith.Documents.UI.Services.DocumentTypes.ViewModels;

/// <summary>
/// Defines the contract for a service that provides query operations for document types.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IIdDescriptionService"/> and provides methods
/// for retrieving and searching document type information asynchronously.
/// The service supports operations such as getting detailed information about a specific document type,
/// retrieving paginated lists of document type summaries, and searching document types based on text criteria.
/// </remarks>
public interface IDocumentTypeQueryService : IIdDescriptionService
{
    /// <summary>
    /// Retrieves the detailed information for a document type with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier of the document type.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document type details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the document type with the specified ID is not found.</exception>
    Task<DocumentTypeDetailsViewModel> GetDetailsAsync(string id);

    /// <summary>
    /// Retrieves summaries for all document types in the system.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of document type summaries.</returns>
    /// <remarks>
    /// This method is equivalent to calling <see cref="GetSummariesAsync(int, int)"/> with skip = 0 and count = 0,
    /// which retrieves all document types without pagination.
    /// </remarks>
    Task<IEnumerable<DocumentTypeSummaryViewModel>> GetSummariesAsync()
        => GetSummariesAsync(0, 0);

    /// <summary>
    /// Retrieves a paginated list of document type summaries.
    /// </summary>
    /// <param name="skip">The number of document types to skip in the result set. Must be non-negative.</param>
    /// <param name="count">The maximum number of document types to return. If 0, returns all document types.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of document type summaries.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="skip"/> is negative.</exception>
    /// <remarks>
    /// This method supports pagination by allowing the caller to specify how many items to skip and how many to return.
    /// When count is 0, the method returns all document types starting from the skip position.
    /// </remarks>
    Task<IEnumerable<DocumentTypeSummaryViewModel>> GetSummariesAsync(int skip, int count);

    /// <summary>
    /// Searches for document types based on the provided search text.
    /// </summary>
    /// <param name="searchText">The text to search for in document type information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of matching document type summaries.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="searchText"/> is null or empty.</exception>
    /// <remarks>
    /// The search is performed on relevant fields of the document type (such as name, description, or other searchable attributes).
    /// The exact search algorithm and matched fields depend on the implementation.
    /// The search is typically case-insensitive and may support partial matches.
    /// </remarks>
    Task<IEnumerable<DocumentTypeSummaryViewModel>> SearchSummariesAsync(string searchText);
}