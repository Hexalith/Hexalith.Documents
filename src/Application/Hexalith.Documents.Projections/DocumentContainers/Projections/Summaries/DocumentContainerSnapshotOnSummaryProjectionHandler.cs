namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for document container snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class DocumentContainerSnapshotOnSummaryProjectionHandler(IProjectionFactory<DocumentContainerSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DocumentContainerAggregateName)
        {
            return;
        }

        DocumentContainerSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentContainer documentContainer = baseEvent.GetAggregate<DocumentContainer>();
        DocumentContainerSummaryViewModel newValue = new(documentContainer.Id, documentContainer.Name, documentContainer.Disabled);
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