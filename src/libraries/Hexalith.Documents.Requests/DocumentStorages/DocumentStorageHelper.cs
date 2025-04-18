namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using Hexalith.Application.Requests;

/// <summary>
/// Provides helper methods for document storage requests.
/// </summary>
public static class DocumentStorageHelper
{
    /// <summary>
    /// Finds the document storage details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document storage identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document storage details view model.</returns>
    public static async Task<DocumentStorageDetailsViewModel?> FindDocumentStorageDetailsAsync(
        [NotNull] this IRequestService requestService,
        string? id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.FindDetailsAsync<DocumentStorageDetailsViewModel, GetDocumentStorageDetails>(
            id,
            user,
            (id) => new GetDocumentStorageDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Finds the document storage summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document storage identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document storage summary view model.</returns>
    public static async Task<DocumentStorageSummaryViewModel?> FindDocumentStorageSummaryAsync(
       [NotNull] this IRequestService requestService,
       string? id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.FindSummaryAsync<DocumentStorageSummaryViewModel, GetDocumentStorageSummaries>(
           id,
           user,
           (id) => new GetDocumentStorageSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);

    /// <summary>
    /// Gets the document storage details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document storage identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document storage details view model.</returns>
    public static async Task<DocumentStorageDetailsViewModel> GetDocumentStorageDetailsAsync(
        [NotNull] this IRequestService requestService,
        [NotNull] string id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.GetDetailsAsync<DocumentStorageDetailsViewModel, GetDocumentStorageDetails>(
            id,
            user,
            (id) => new GetDocumentStorageDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Gets the document storage summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document storage identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document storage summary view model.</returns>
    public static async Task<DocumentStorageSummaryViewModel> GetDocumentStorageSummaryAsync(
       [NotNull] this IRequestService requestService,
       [NotNull] string id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.GetSummaryAsync<DocumentStorageSummaryViewModel, GetDocumentStorageSummaries>(
           id,
           user,
           (id) => new GetDocumentStorageSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);
}