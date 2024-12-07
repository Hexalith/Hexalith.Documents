namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Collections;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Domain;
using Hexalith.Domain.Events;

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
public partial class FileTypeSnapshotOnIdsCollectionProjectionHandler(
    IProjectionFactory<IEnumerable<string>> factory,
    ILogger<FileTypeSnapshotOnIdsCollectionProjectionHandler> logger)
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

        IEnumerable<string> currentValue = await factory
            .GetStateAsync(DocumentUIConstants.FileTypeIdsCollectionProjectionName, cancellationToken)
            .ConfigureAwait(false)
            ?? [];

        if (currentValue.Any(p => p == metadata.AggregateGlobalId))
        {
            return;
        }

        await factory
            .SetStateAsync(
                metadata.AggregateGlobalId,
                currentValue.Append(metadata.AggregateGlobalId).Distinct().OrderBy(p => p),
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
        Message = "The file type identifiers collection did not contain id '{AggregateGlobalId}' and needed to updated with a snapshot. MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogProjectionSynchronizedWarning(
        ILogger logger,
        string? aggregateGlobalId,
        string? messageId,
        string? correlationId);
}