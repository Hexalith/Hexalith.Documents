namespace Hexalith.Documents.UI.Services.DocumentInformationExtractions.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;
using Hexalith.Domain.Events;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the snapshot events for document information extraction details.
/// </summary>
public partial class DocumentInformationExtractionDetailsSnapshotHandler(
    IProjectionFactory<DocumentInformationExtractionDetailsViewModel> factory,
    ILogger<DocumentInformationExtractionDetailsSnapshotHandler> logger) : IProjectionUpdateHandler<SnapshotEvent>
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

        DocumentInformationExtractionDetailsViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentInformationExtraction documentInformationExtraction = baseEvent.GetAggregate<DocumentInformationExtraction>();
        DocumentInformationExtractionDetailsViewModel newValue = new(
                documentInformationExtraction.Id,
                documentInformationExtraction.Name,
                documentInformationExtraction.Model,
                documentInformationExtraction.SystemMessage,
                documentInformationExtraction.OutputFormat,
                documentInformationExtraction.OutputSample,
                documentInformationExtraction.Instructions,
                documentInformationExtraction.ValidationModel,
                documentInformationExtraction.ValidationInstructions,
                documentInformationExtraction.Comments,
                false);

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

        LogProjectionSynchronizedWarning(
            logger,
            metadata.AggregateGlobalId,
            metadata.Message.Id,
            metadata.Context.CorrelationId);
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Warning,
        Message = "The document information extraction details view model with id '{AggregateGlobalId}' was outdated and needed to be synchronized with a snapshot. MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogProjectionSynchronizedWarning(
        ILogger logger,
        string? aggregateGlobalId,
        string? messageId,
        string? correlationId);
}