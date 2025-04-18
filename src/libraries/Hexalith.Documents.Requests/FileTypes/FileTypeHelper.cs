// <copyright file="FileTypeHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.FileTypes;

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

using Hexalith.Application.Requests;

/// <summary>
/// Provides helper methods for file type requests.
/// </summary>
public static class FileTypeHelper
{
    /// <summary>
    /// Finds the file type details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The file type identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the file type details view model.</returns>
    public static async Task<FileTypeDetailsViewModel?> FindFileTypeDetailsAsync(
        [NotNull] this IRequestService requestService,
        string? id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.FindDetailsAsync<FileTypeDetailsViewModel, GetFileTypeDetails>(
            id,
            user,
            (id) => new GetFileTypeDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Finds the file type summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The file type identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the file type summary view model.</returns>
    public static async Task<FileTypeSummaryViewModel?> FindFileTypeSummaryAsync(
       [NotNull] this IRequestService requestService,
       string? id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.FindSummaryAsync<FileTypeSummaryViewModel, GetFileTypeSummaries>(
           id,
           user,
           (id) => new GetFileTypeSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);

    /// <summary>
    /// Gets the file type details asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The file type identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the file type details view model.</returns>
    public static async Task<FileTypeDetailsViewModel> GetFileTypeDetailsAsync(
        [NotNull] this IRequestService requestService,
        [NotNull] string id,
        [NotNull] ClaimsPrincipal user,
        CancellationToken cancellationToken) => await requestService.GetDetailsAsync<FileTypeDetailsViewModel, GetFileTypeDetails>(
            id,
            user,
            (id) => new GetFileTypeDetails(id),
            cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Gets the file type summary asynchronously.
    /// </summary>
    /// <param name="requestService">The request service.</param>
    /// <param name="id">The file type identifier.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the file type summary view model.</returns>
    public static async Task<FileTypeSummaryViewModel> GetFileTypeSummaryAsync(
       [NotNull] this IRequestService requestService,
       [NotNull] string id,
       [NotNull] ClaimsPrincipal user,
       CancellationToken cancellationToken) => await requestService.GetSummaryAsync<FileTypeSummaryViewModel, GetFileTypeSummaries>(
           id,
           user,
           (id) => new GetFileTypeSummaries([id]),
           cancellationToken)
           .ConfigureAwait(false);
}