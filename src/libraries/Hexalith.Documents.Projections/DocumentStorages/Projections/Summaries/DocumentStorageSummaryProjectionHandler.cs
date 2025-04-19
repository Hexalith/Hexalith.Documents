// <copyright file="DocumentStorageSummaryProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Abstract base class for handling updates to DocumentStorage projections based on events.
/// </summary>
/// <typeparam name="TDocumentStorageEvent">The type of the document partition event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentStorageSummaryProjectionHandler<TDocumentStorageEvent>(IProjectionFactory<DocumentStorageSummaryViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentStorageEvent, DocumentStorageSummaryViewModel>(factory)
    where TDocumentStorageEvent : DocumentStorageEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentStorageEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentStorageSummaryViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentStorageSummaryViewModel? newValue = await ApplyEventAsync(
                baseEvent,
                currentValue,
                cancellationToken)
            .ConfigureAwait(false);
        if (newValue == null)
        {
            return;
        }

        await SaveProjectionAsync(metadata.AggregateGlobalId, newValue, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Applies the event to the document partition summary view model.
    /// </summary>
    /// <param name="baseEvent">The document partition event.</param>
    /// <param name="summary">The existing document partition summary view model, if any.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document partition summary view model.</returns>
    protected abstract Task<DocumentStorageSummaryViewModel?> ApplyEventAsync(TDocumentStorageEvent baseEvent, DocumentStorageSummaryViewModel? summary, CancellationToken cancellationToken);
}