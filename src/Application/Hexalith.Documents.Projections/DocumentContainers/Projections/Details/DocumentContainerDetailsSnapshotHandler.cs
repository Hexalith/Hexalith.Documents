namespace Hexalith.Documents.UI.Services.DocumentContainers.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Domain.Events;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the snapshot events for document container details.
/// </summary>
public partial class DocumentContainerDetailsSnapshotHandler(
    IProjectionFactory<DocumentContainerDetailsViewModel> factory,
    ILogger<DocumentContainerDetailsSnapshotHandler> logger) : IProjectionUpdateHandler<SnapshotEvent>
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

        DocumentContainerDetailsViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentContainer documentContainer = baseEvent.GetAggregate<DocumentContainer>();
        DocumentContainerDetailsViewModel newValue = new(
            documentContainer.Id,
            documentContainer.Name,
            documentContainer.Description,
            documentContainer.AutomaticRoutingInstructions,
            documentContainer.Actors,
            documentContainer.FileTypeIds,
            documentContainer.Tags,
            documentContainer.Disabled);
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
        Message = "The document container details view model with id '{AggregateGlobalId}' was outdated and needed to be synchronized with a snapshot. MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogProjectionSynchronizedWarning(
        ILogger logger,
        string? aggregateGlobalId,
        string? messageId,
        string? correlationId);
}