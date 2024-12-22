namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Documents.Requests.DocumentPartitions;

/// <summary>
/// Abstract base class for handling updates to DocumentPartition projections based on events.
/// </summary>
/// <typeparam name="TDocumentPartitionEvent">The type of the document partition event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentPartitionSummaryProjectionHandler<TDocumentPartitionEvent>(IProjectionFactory<DocumentPartitionSummaryViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentPartitionEvent, DocumentPartitionSummaryViewModel>(factory)
    where TDocumentPartitionEvent : DocumentPartitionEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentPartitionEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentPartitionSummaryViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentPartitionSummaryViewModel? newValue = await ApplyEventAsync(
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
    protected abstract Task<DocumentPartitionSummaryViewModel?> ApplyEventAsync(TDocumentPartitionEvent baseEvent, DocumentPartitionSummaryViewModel? summary, CancellationToken cancellationToken);
}