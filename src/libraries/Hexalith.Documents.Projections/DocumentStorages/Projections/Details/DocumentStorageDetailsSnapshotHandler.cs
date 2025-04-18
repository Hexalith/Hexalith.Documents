namespace Hexalith.Documents.UI.Services.DocumentStorages.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Domain.Events;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the snapshot events for document partition details.
/// </summary>
public partial class DocumentStorageDetailsSnapshotHandler(
    IProjectionFactory<DocumentStorageDetailsViewModel> factory,
    ILogger<DocumentStorageDetailsSnapshotHandler> logger) : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DocumentStorageAggregateName)
        {
            return;
        }

        DocumentStorageDetailsViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentStorage documentPartition = baseEvent.GetAggregate<DocumentStorage>();
        DocumentStorageDetailsViewModel newValue = new(
            documentPartition.Id,
            documentPartition.Name,
            documentPartition.StorageType,
            documentPartition.Description,
            documentPartition.ConnectionString,
            documentPartition.Disabled);
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
        Message = "The document partition details view model with id '{AggregateGlobalId}' was outdated and needed to be synchronized with a snapshot. MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogProjectionSynchronizedWarning(
        ILogger logger,
        string? aggregateGlobalId,
        string? messageId,
        string? correlationId);
}