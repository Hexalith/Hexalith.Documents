// <copyright file="DocumentTypeAddedOnSummaryProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a document type is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentTypeAddedOnSummaryProjectionHandler(IProjectionFactory<DocumentTypeSummaryViewModel> factory)
    : DocumentTypeSummaryProjectionHandler<DocumentTypeAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeSummaryViewModel?> ApplyEventAsync([NotNull] DocumentTypeAdded baseEvent, DocumentTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentTypeSummaryViewModel?>(new DocumentTypeSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
    }
}