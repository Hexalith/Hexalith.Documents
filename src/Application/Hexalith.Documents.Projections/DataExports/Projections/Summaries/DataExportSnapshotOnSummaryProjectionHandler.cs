namespace Hexalith.Documents.Projections.DataExports.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DataExports;
using Hexalith.Documents.Requests.DataExports;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for data export snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class DataExportSnapshotOnSummaryProjectionHandler(IProjectionFactory<DataExportSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DataExportAggregateName)
        {
            return;
        }

        DataExportSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DataExport dataExport = baseEvent.GetAggregate<DataExport>();
        DataExportSummaryViewModel newValue = new(dataExport.Id, dataExport.Size, dataExport.StartedAt, dataExport.CompletedAt);
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