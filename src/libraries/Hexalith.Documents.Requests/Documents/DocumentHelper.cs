namespace Hexalith.Documents.Requests.Documents
    ;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using Hexalith.Application.Requests;

/// <summary>
/// Provides helper methods for document requests.
/// </summary>
public static class DocumentHelper
{
    /// <summary>
    /// Finds the document details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document details view model.</returns>
    public static async Task<DocumentDetailsViewModel?> FindDocumentDetailsAsync(
        [NotNull] this IRequestService requestService,
        string? id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.FindDetailsAsync<DocumentDetailsViewModel, GetDocumentDetails>(
            id,
            user,
            (id) => new GetDocumentDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Finds the document summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document summary view model.</returns>
    public static async Task<DocumentSummaryViewModel?> FindDocumentSummaryAsync(
       [NotNull] this IRequestService requestService,
       string? id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.FindSummaryAsync<DocumentSummaryViewModel, GetDocumentSummaries>(
           id,
           user,
           (id) => new GetDocumentSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);

    /// <summary>
    /// Gets the document details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document details view model.</returns>
    public static async Task<DocumentDetailsViewModel> GetDocumentDetailsAsync(
        [NotNull] this IRequestService requestService,
        [NotNull] string id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.GetDetailsAsync<DocumentDetailsViewModel, GetDocumentDetails>(
            id,
            user,
            (id) => new GetDocumentDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Gets the document summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document summary view model.</returns>
    public static async Task<DocumentSummaryViewModel> GetDocumentSummaryAsync(
       [NotNull] this IRequestService requestService,
       [NotNull] string id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.GetSummaryAsync<DocumentSummaryViewModel, GetDocumentSummaries>(
           id,
           user,
           (id) => new GetDocumentSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);
}