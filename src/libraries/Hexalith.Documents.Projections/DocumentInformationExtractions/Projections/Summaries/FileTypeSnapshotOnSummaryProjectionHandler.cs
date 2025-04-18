namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for document information extraction snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public partial class DocumentInformationExtractionSnapshotOnSummaryProjectionHandler(IProjectionFactory<DocumentInformationExtractionSummaryViewModel> factory)
    : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DocumentInformationExtractionAggregateName)
        {
            return;
        }

        DocumentInformationExtractionSummaryViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentInformationExtraction documentInformationExtraction = baseEvent.GetAggregate<DocumentInformationExtraction>();
        DocumentInformationExtractionSummaryViewModel newValue = new(documentInformationExtraction.Id, documentInformationExtraction.Name, documentInformationExtraction.Disabled);
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