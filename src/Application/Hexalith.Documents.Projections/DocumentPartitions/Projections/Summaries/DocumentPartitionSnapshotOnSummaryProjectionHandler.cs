namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DocumentPartitions;
using Hexalith.Documents.Requests.DocumentPartitions;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for document partition snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class DocumentPartitionSnapshotOnSummaryProjectionHandler(IProjectionFactory<DocumentPartitionSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DocumentPartitionAggregateName)
        {
            return;
        }

        DocumentPartitionSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentPartition documentPartition = baseEvent.GetAggregate<DocumentPartition>();
        DocumentPartitionSummaryViewModel newValue = new(documentPartition.Id, documentPartition.Name, documentPartition.Disabled);
        if (currentValue is not null && currentValue == newValue)
        {
            return;
        }

        await factory
            .SetStateAsync(
                metadata.AggregateGlobalId,
                newValue,
                cancellationToken)
            .ConfigureAwait(false);
    }
}