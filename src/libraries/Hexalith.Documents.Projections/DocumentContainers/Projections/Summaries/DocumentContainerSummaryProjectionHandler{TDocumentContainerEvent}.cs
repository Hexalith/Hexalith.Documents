namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Abstract base class for handling updates to DocumentContainer projections based on events.
/// </summary>
/// <typeparam name="TDocumentContainerEvent">The type of the document container event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentContainerSummaryProjectionHandler<TDocumentContainerEvent>(IProjectionFactory<DocumentContainerSummaryViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentContainerEvent, DocumentContainerSummaryViewModel>(factory)
    where TDocumentContainerEvent : DocumentContainerEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentContainerEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentContainerSummaryViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentContainerSummaryViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the document container summary view model.
    /// </summary>
    /// <param name="baseEvent">The document container event.</param>
    /// <param name="summary">The existing document container summary view model, if any.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document container summary view model.</returns>
    protected abstract Task<DocumentContainerSummaryViewModel?> ApplyEventAsync(TDocumentContainerEvent baseEvent, DocumentContainerSummaryViewModel? summary, CancellationToken cancellationToken);
}