namespace Hexalith.Documents.Projections.DataManagements.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DataManagements;
using Hexalith.Documents.Requests.DataManagements;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for data export snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class DataManagementSnapshotOnSummaryProjectionHandler(IProjectionFactory<DataManagementSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DataManagementAggregateName)
        {
            return;
        }

        DataManagementSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DataManagement dataExport = baseEvent.GetAggregate<DataManagement>();
        DataManagementSummaryViewModel newValue = new(dataExport.Id, dataExport.Size, dataExport.StartedAt, dataExport.CompletedAt);
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