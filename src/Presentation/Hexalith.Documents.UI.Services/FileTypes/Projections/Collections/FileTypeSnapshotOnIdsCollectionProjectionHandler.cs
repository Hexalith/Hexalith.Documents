namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Collections;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Services;
using Hexalith.Documents.Domain;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection of file type snapshots on IDs collection.
/// </summary>
/// <param name="factory">The factory.</param>
public partial class FileTypeSnapshotOnIdsCollectionProjectionHandler(IIdCollectionFactory factory) : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        IIdCollectionService service = factory.CreateService(
            DocumentUIConstants.FileTypeIdsProjectionName,
            metadata.Context.PartitionId);
        if (baseEvent?.AggregateName != DocumentDomainHelper.FileTypeAggregateName)
        {
            // Since the snapshot event is the same for all aggregates, we need to check the aggregate name.
            return;
        }

        await service.AddAsync(metadata.AggregateGlobalId, cancellationToken).ConfigureAwait(false);
    }
}