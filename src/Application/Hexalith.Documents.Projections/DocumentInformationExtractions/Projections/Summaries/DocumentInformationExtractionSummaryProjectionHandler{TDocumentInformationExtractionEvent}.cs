﻿namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Abstract base class for handling updates to DocumentInformationExtraction projections based on events.
/// </summary>
/// <typeparam name="TDocumentInformationExtractionEvent">The type of the document information extraction event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentInformationExtractionSummaryProjectionHandler<TDocumentInformationExtractionEvent>(IProjectionFactory<DocumentInformationExtractionSummaryViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentInformationExtractionEvent, DocumentInformationExtractionSummaryViewModel>(factory)
    where TDocumentInformationExtractionEvent : DocumentInformationExtractionEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentInformationExtractionEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentInformationExtractionSummaryViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentInformationExtractionSummaryViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the document information extraction summary view model.
    /// </summary>
    /// <param name="baseEvent">The document information extraction event.</param>
    /// <param name="summary">The existing document information extraction summary view model, if any.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document information extraction summary view model.</returns>
    protected abstract Task<DocumentInformationExtractionSummaryViewModel?> ApplyEventAsync(TDocumentInformationExtractionEvent baseEvent, DocumentInformationExtractionSummaryViewModel? summary, CancellationToken cancellationToken);
}