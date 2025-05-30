// <copyright file="FileTypeDetailsProjectionHandler{TFileTypeEvent}.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Abstract base class for handling updates to FileType projections based on events.
/// </summary>
/// <typeparam name="TFileTypeEvent">The type of the file type event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class FileTypeDetailsProjectionHandler<TFileTypeEvent>(IProjectionFactory<FileTypeDetailsViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TFileTypeEvent, FileTypeDetailsViewModel>(factory)
    where TFileTypeEvent : FileTypeEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TFileTypeEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        FileTypeDetailsViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        FileTypeDetailsViewModel? newValue = await ApplyEventAsync(
                baseEvent,
                currentValue,
                cancellationToken)
            .ConfigureAwait(false);
        if (newValue == null || newValue == currentValue)
        {
            return;
        }

        await SaveProjectionAsync(metadata.AggregateGlobalId, newValue, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Applies the event to the file type summary view model.
    /// </summary>
    /// <param name="baseEvent">The file type event.</param>
    /// <param name="model">The current file type detail view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated file type summary view model.</returns>
    protected abstract Task<FileTypeDetailsViewModel?> ApplyEventAsync(TFileTypeEvent baseEvent, FileTypeDetailsViewModel? model, CancellationToken cancellationToken);
}