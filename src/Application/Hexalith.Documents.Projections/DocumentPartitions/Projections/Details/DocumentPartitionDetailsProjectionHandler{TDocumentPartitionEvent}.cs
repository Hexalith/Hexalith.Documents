namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Details;

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
public abstract class DocumentPartitionDetailsProjectionHandler<TDocumentPartitionEvent>(IProjectionFactory<DocumentPartitionDetailsViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentPartitionEvent, DocumentPartitionDetailsViewModel>(factory)
    where TDocumentPartitionEvent : DocumentPartitionEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentPartitionEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentPartitionDetailsViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentPartitionDetailsViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the document partition summary view model.
    /// </summary>
    /// <param name="baseEvent">The document partition event.</param>
    /// <param name="model">The current document partition detail view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document partition summary view model.</returns>
    protected abstract Task<DocumentPartitionDetailsViewModel?> ApplyEventAsync(TDocumentPartitionEvent baseEvent, DocumentPartitionDetailsViewModel? model, CancellationToken cancellationToken);
}