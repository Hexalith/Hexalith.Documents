namespace Hexalith.Documents.Projections.FileTypes.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for file type snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class FileTypeSnapshotOnSummaryProjectionHandler(IProjectionFactory<FileTypeSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.FileTypeAggregateName)
        {
            return;
        }

        FileTypeSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        FileType fileType = baseEvent.GetAggregate<FileType>();
        FileTypeSummaryViewModel newValue = new(fileType.Id, fileType.Name, fileType.ContentType, fileType.FileExtension, fileType.Disabled);
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