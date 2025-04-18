namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for document type snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class DocumentTypeSnapshotOnSummaryProjectionHandler(IProjectionFactory<DocumentTypeSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DocumentTypeAggregateName)
        {
            return;
        }

        DocumentTypeSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentType documentType = baseEvent.GetAggregate<DocumentType>();
        DocumentTypeSummaryViewModel newValue = new(documentType.Id, documentType.Name, documentType.Disabled);
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