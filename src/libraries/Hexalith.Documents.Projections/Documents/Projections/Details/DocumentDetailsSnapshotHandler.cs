// <copyright file="DocumentDetailsSnapshotHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.UI.Services.Documents.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents;
using Hexalith.Documents.Documents;
using Hexalith.Documents.Requests.Documents;
using Hexalith.Domain.Events;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the snapshot events for document details.
/// </summary>
/// <param name="factory">The projection factory for document details view models.</param>
/// <param name="logger">The logger for the handler.</param>
public partial class DocumentDetailsSnapshotHandler(
    IProjectionFactory<DocumentDetailsViewModel> factory,
    ILogger<DocumentDetailsSnapshotHandler> logger) : IProjectionUpdateHandler<SnapshotEvent>
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

        DocumentDetailsViewModel? currentValue = await factory
            .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        Document document = baseEvent.GetAggregate<Document>();
        DocumentDetailsViewModel newValue = new(
            document.Id,
            document.Description,
            document.Routing,
            document.ParentDocumentId,
            document.State,
            document.Actors,
            document.Files,
            document.Tags,
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

        LogProjectionSynchronizedWarning(
            logger,
            metadata.AggregateGlobalId,
            metadata.Message.Id,
            metadata.Context.CorrelationId);
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Warning,
        Message = "The document details view model with id '{AggregateGlobalId}' was outdated and needed to be synchronized with a snapshot. MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogProjectionSynchronizedWarning(
        ILogger logger,
        string? aggregateGlobalId,
        string? messageId,
        string? correlationId);
}