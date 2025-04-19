// <copyright file="DocumentDescriptionChangedOnSummaryProjectionHandler.cs" company="ITANEO">
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
/// Handles the projection update when a document description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentDescriptionChangedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentDescriptionChangedOnSummaryProjectionHandler(IProjectionFactory<DocumentSummaryViewModel> factory)
    : DocumentSummaryProjectionHandler<DocumentDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentSummaryViewModel?> ApplyEventAsync([NotNull] DocumentDescriptionChanged baseEvent, DocumentSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentSummaryViewModel?>(summary with { Name = baseEvent.Name });
    }
}