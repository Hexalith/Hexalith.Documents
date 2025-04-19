namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents;
using Hexalith.Documents.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for document partition snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class DocumentStorageSnapshotOnSummaryProjectionHandler(IProjectionFactory<DocumentStorageSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DocumentStorageAggregateName)
        {
            return;
        }

        DocumentStorageSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentStorage documentPartition = baseEvent.GetAggregate<DocumentStorage>();
        DocumentStorageSummaryViewModel newValue = new(documentPartition.Id, documentPartition.Name, documentPartition.Disabled);
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