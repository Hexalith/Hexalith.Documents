// <copyright file="DocumentStorageDescriptionChangedOnSummaryProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handles the projection update when a document partition description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentStorageDescriptionChangedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentStorageDescriptionChangedOnSummaryProjectionHandler(IProjectionFactory<DocumentStorageSummaryViewModel> factory)
    : DocumentStorageSummaryProjectionHandler<DocumentStorageDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentStorageSummaryViewModel?> ApplyEventAsync([NotNull] DocumentStorageDescriptionChanged baseEvent, DocumentStorageSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentStorageSummaryViewModel?>(new DocumentStorageSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
        }

        return Task.FromResult<DocumentStorageSummaryViewModel?>(summary with { Name = baseEvent.Name });
    }
}