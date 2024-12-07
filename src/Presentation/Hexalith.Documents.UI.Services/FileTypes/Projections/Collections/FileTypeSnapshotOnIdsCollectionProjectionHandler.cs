namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Collections;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection of file type snapshots on IDs collection.
/// </summary>
/// <param name="factory">The factory.</param>
public partial class FileTypeSnapshotOnIdsCollectionProjectionHandler(IProjectionFactory<IdCollection> factory)
    : IdsCollectionProjectionHandler<SnapshotEvent>(factory)
{
    /// <inheritdoc/>
    public override async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.FileTypeAggregateName)
        {
            return;
        }

        await base.ApplyAsync(baseEvent, metadata, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override bool IsRemoveEvent(SnapshotEvent baseEvent) => false;
}