namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using Hexalith.Application.Requests;

/// <summary>
/// Provides helper methods for document type requests.
/// </summary>
public static class DocumentTypeHelper
{
    /// <summary>
    /// Finds the document type details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document type identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document type details view model.</returns>
    public static async Task<DocumentTypeDetailsViewModel?> FindDocumentTypeDetailsAsync(
        [NotNull] this IRequestService requestService,
        string? id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.FindDetailsAsync<DocumentTypeDetailsViewModel, GetDocumentTypeDetails>(
            id,
            user,
            (id) => new GetDocumentTypeDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Finds the document type summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document type identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document type summary view model.</returns>
    public static async Task<DocumentTypeSummaryViewModel?> FindDocumentTypeSummaryAsync(
       [NotNull] this IRequestService requestService,
       string? id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.FindSummaryAsync<DocumentTypeSummaryViewModel, GetDocumentTypeSummaries>(
           id,
           user,
           (id) => new GetDocumentTypeSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);

    /// <summary>
    /// Gets the document type details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document type identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document type details view model.</returns>
    public static async Task<DocumentTypeDetailsViewModel> GetDocumentTypeDetailsAsync(
        [NotNull] this IRequestService requestService,
        [NotNull] string id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.GetDetailsAsync<DocumentTypeDetailsViewModel, GetDocumentTypeDetails>(
            id,
            user,
            (id) => new GetDocumentTypeDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Gets the document type summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The document type identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document type summary view model.</returns>
    public static async Task<DocumentTypeSummaryViewModel> GetDocumentTypeSummaryAsync(
       [NotNull] this IRequestService requestService,
       [NotNull] string id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.GetSummaryAsync<DocumentTypeSummaryViewModel, GetDocumentTypeSummaries>(
           id,
           user,
           (id) => new GetDocumentTypeSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);
}