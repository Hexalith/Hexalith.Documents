// <copyright file="DocumentInformationExtractionSnapshotOnSummaryProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Summaries;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents;
using Hexalith.Documents.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;
using Hexalith.Domain.Events;

/// <summary>
/// Handles the projection updates for document information extraction snapshots on summary.
/// </summary>
/// <param name="factory">The projection factory.</param>
public class DocumentInformationExtractionSnapshotOnSummaryProjectionHandler(IProjectionFactory<DocumentInformationExtractionSummaryViewModel> factory)
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