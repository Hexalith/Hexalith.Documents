namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using Hexalith.Application.Requests;

/// <summary>
/// Provides helper methods for document container requests.
/// </summary>
public static class DocumentContainerHelper
{
    /// <summary>
    /// Finds the document container details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document container identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document container details view model.</returns>
    public static async Task<DocumentContainerDetailsViewModel?> FindDocumentContainerDetailsAsync(
        [NotNull] this IRequestService requestService,
        string? id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.FindDetailsAsync<DocumentContainerDetailsViewModel, GetDocumentContainerDetails>(
            id,
            user,
            (id) => new GetDocumentContainerDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Finds the document container summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document container identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document container summary view model.</returns>
    public static async Task<DocumentContainerSummaryViewModel?> FindDocumentContainerSummaryAsync(
       [NotNull] this IRequestService requestService,
       string? id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.FindSummaryAsync<DocumentContainerSummaryViewModel, GetDocumentContainerSummaries>(
           id,
           user,
           (id) => new GetDocumentContainerSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);

    /// <summary>
    /// Gets the document container details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document container identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document container details view model.</returns>
    public static async Task<DocumentContainerDetailsViewModel> GetDocumentContainerDetailsAsync(
        [NotNull] this IRequestService requestService,
        [NotNull] string id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.GetDetailsAsync<DocumentContainerDetailsViewModel, GetDocumentContainerDetails>(
            id,
            user,
            (id) => new GetDocumentContainerDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Gets the document container summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document container identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document container summary view model.</returns>
    public static async Task<DocumentContainerSummaryViewModel> GetDocumentContainerSummaryAsync(
       [NotNull] this IRequestService requestService,
       [NotNull] string id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.GetSummaryAsync<DocumentContainerSummaryViewModel, GetDocumentContainerSummaries>(
           id,
           user,
           (id) => new GetDocumentContainerSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);
}