namespace Hexalith.Documents.Projections.Documents.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Abstract base class for handling updates to Document projections based on events.
/// </summary>
/// <typeparam name="TDocumentEvent">The type of the document event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentSummaryProjectionHandler<TDocumentEvent>(IProjectionFactory<DocumentSummaryViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentEvent, DocumentSummaryViewModel>(factory)
    where TDocumentEvent : DocumentEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentSummaryViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentSummaryViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the document summary view model.
    /// </summary>
    /// <param name="baseEvent">The document event.</param>
    /// <param name="summary">The existing document summary view model, if any.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document summary view model.</returns>
    protected abstract Task<DocumentSummaryViewModel?> ApplyEventAsync(TDocumentEvent baseEvent, DocumentSummaryViewModel? summary, CancellationToken cancellationToken);
}