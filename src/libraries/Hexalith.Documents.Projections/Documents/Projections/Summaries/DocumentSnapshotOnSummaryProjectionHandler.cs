namespace Hexalith.Documents.Projections.Documents.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents;
using Hexalith.Documents.Documents;
using Hexalith.Documents.Requests.Documents;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for document snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class DocumentSnapshotOnSummaryProjectionHandler(IProjectionFactory<DocumentSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DocumentAggregateName)
        {
            return;
        }

        DocumentSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        Document document = baseEvent.GetAggregate<Document>();
        DocumentSummaryViewModel newValue = new(
            document.Id,
            document.Description.Name,
            document.Description.DocumentContainerId ?? string.Empty,
            document.Files.Sum(p => p.Size),
            document.Disabled);
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