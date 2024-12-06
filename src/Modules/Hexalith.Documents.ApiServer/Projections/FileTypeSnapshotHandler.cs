namespace Hexalith.Documents.ApiServer.Projections;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Domain.Events;
using Hexalith.Infrastructure.DaprRuntime.Projections;

using Microsoft.Extensions.Logging;

/// <summary>
/// Class IntercompanyDropshipDeliveryForCustomerDeselectedHandler.
/// Implements the <see cref="Application.Events.IntegrationEventProjectionUpdateHandler{CustomerRegistered}" />.
/// </summary>
/// <seealso cref="Application.Events.IntegrationEventProjectionUpdateHandler{CustomerRegistered}" />
/// <remarks>
/// Initializes a new instance of the <see cref="FileTypeSnapshotHandler"/> class.
/// </remarks>
/// <param name="stateStoreProvider">The state store provider.</param>
public partial class FileTypeSnapshotHandler(
    IActorProjectionFactory<FileType> fileTypeFactory,
    ILogger<FileTypeSnapshotHandler> logger) : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent is null || baseEvent.AggregateName != DocumentDomainHelper.FileTypeAggregateName || string.IsNullOrWhiteSpace(metadata.AggregateGlobalId))
        {
            LogProjectionEventIgnoredWarning(
                logger,
                metadata.Message.Name,
                metadata.Message.Aggregate.Name,
                metadata.Message.Aggregate.Id,
                metadata.AggregateGlobalId,
                metadata.Message.Id,
                metadata.Context.CorrelationId);
            return;
        }

        FileType fileType = baseEvent.GetAggregate<FileType>();
        await fileTypeFactory
            .SetStateAsync(baseEvent.AggregateId, fileType, cancellationToken)
            .ConfigureAwait(false);
        LogProjectionInitializedWithSnapshotInformation(
            logger,
            baseEvent.AggregateName,
            baseEvent.AggregateId,
            metadata.AggregateGlobalId,
            metadata.Message.Id,
            metadata.Context.CorrelationId);
    }

    [LoggerMessage(
            EventId = 2,
            Level = LogLevel.Warning,
            Message = "Parties snapshot event ignored. EventType='{MessageName}'; AggregateName='{AggregateName}'; AggregateId='{AggregateId}'; GlobalAggregateId='{GlobalAggregateId}'; MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogProjectionEventIgnoredWarning(
        ILogger logger,
        string? messageName,
        string? aggregateName,
        string? aggregateId,
        string? globalAggregateId,
        string? messageId,
        string? correlationId);

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "{AggregateName} with id '{AggregateId}' initialized with a snapshot. GlobalAggregateId='{GlobalAggregateId}'; MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogProjectionInitializedWithSnapshotInformation(
        ILogger logger,
        string aggregateName,
        string aggregateId,
        string? globalAggregateId,
        string? messageId,
        string? correlationId);
}