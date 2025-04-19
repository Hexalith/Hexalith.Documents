// <copyright file="DocumentDetailsProjectionHandler{TDocumentEvent}.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.Documents.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Abstract base class for handling updates to Document projections based on events.
/// </summary>
/// <typeparam name="TDocumentEvent">The type of the document event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentDetailsProjectionHandler<TDocumentEvent>(IProjectionFactory<DocumentDetailsViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentEvent, DocumentDetailsViewModel>(factory)
    where TDocumentEvent : DocumentEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentDetailsViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentDetailsViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the document summary view model.
    /// </summary>
    /// <param name="baseEvent">The document event.</param>
    /// <param name="model">The current document detail view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document summary view model.</returns>
    protected abstract Task<DocumentDetailsViewModel?> ApplyEventAsync(TDocumentEvent baseEvent, DocumentDetailsViewModel? model, CancellationToken cancellationToken);
}