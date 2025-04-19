// <copyright file="DocumentDisabledOnSummaryProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.Documents.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handles the projection update when a document is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentDisabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentDisabledOnSummaryProjectionHandler(IProjectionFactory<DocumentSummaryViewModel> factory)
    : DocumentSummaryProjectionHandler<DocumentDisabled>(factory)
{
    /// <summary>
    /// Applies the document disabled event to the summary view model.
    /// </summary>
    /// <param name="baseEvent">The document disabled event.</param>
    /// <param name="summary">The current summary view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary view model.</returns>
    protected override Task<DocumentSummaryViewModel?> ApplyEventAsync([NotNull] DocumentDisabled baseEvent, DocumentSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentSummaryViewModel?>(summary with { Disabled = true });
    }
}