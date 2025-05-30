﻿// <copyright file="DocumentTypeSummaryProjectionHandler{TDocumentTypeEvent}.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Abstract base class for handling updates to DocumentType projections based on events.
/// </summary>
/// <typeparam name="TDocumentTypeEvent">The type of the document type event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentTypeSummaryProjectionHandler<TDocumentTypeEvent>(IProjectionFactory<DocumentTypeSummaryViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentTypeEvent, DocumentTypeSummaryViewModel>(factory)
    where TDocumentTypeEvent : DocumentTypeEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentTypeEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentTypeSummaryViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentTypeSummaryViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the document type summary view model.
    /// </summary>
    /// <param name="baseEvent">The document type event.</param>
    /// <param name="summary">The existing document type summary view model, if any.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document type summary view model.</returns>
    protected abstract Task<DocumentTypeSummaryViewModel?> ApplyEventAsync(TDocumentTypeEvent baseEvent, DocumentTypeSummaryViewModel? summary, CancellationToken cancellationToken);
}