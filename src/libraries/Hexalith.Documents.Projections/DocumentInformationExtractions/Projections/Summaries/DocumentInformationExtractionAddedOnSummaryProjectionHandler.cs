// <copyright file="DocumentInformationExtractionAddedOnSummaryProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Handles the projection update when a document information extraction is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentInformationExtractionAddedOnSummaryProjectionHandler(IProjectionFactory<DocumentInformationExtractionSummaryViewModel> factory)
    : DocumentInformationExtractionSummaryProjectionHandler<DocumentInformationExtractionAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentInformationExtractionSummaryViewModel?> ApplyEventAsync([NotNull] DocumentInformationExtractionAdded baseEvent, DocumentInformationExtractionSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentInformationExtractionSummaryViewModel?>(new DocumentInformationExtractionSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
    }
}