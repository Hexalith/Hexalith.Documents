// <copyright file="DataManagementDetailsSnapshotHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.UI.Services.DataManagements.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.DataManagements;
using Hexalith.Documents.Requests.DataManagements;
using Hexalith.Domain.Events;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the snapshot events for data export details.
/// </summary>
public partial class DataManagementDetailsSnapshotHandler(
    IProjectionFactory<DataManagementDetailsViewModel> factory,
    ILogger<DataManagementDetailsSnapshotHandler> logger) : IProjectionUpdateHandler<SnapshotEvent>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(SnapshotEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        if (baseEvent?.AggregateName != DocumentDomainHelper.DataManagementAggregateName)
        {
            return;
        }

        DataManagementDetailsViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DataManagement dataExport = baseEvent.GetAggregate<DataManagement>();
        DataManagementDetailsViewModel newValue = new(
            dataExport.Id,
            dataExport.Size,
            dataExport.Comments,
            dataExport.StartedAt,
            dataExport.CompletedAt);
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
        Message = "The data export details view model with id '{AggregateGlobalId}' was outdated and needed to be synchronized with a snapshot. MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogProjectionSynchronizedWarning(
        ILogger logger,
        string? aggregateGlobalId,
        string? messageId,
        string? correlationId);
}