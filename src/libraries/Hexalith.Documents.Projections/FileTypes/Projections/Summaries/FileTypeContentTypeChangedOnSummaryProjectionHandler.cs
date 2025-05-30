// <copyright file="FileTypeContentTypeChangedOnSummaryProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.FileTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when the content type of a file type is changed.
/// </summary>
/// <param name="factory">The projection factory for file type summary view models.</param>
public class FileTypeContentTypeChangedOnSummaryProjectionHandler(IProjectionFactory<FileTypeSummaryViewModel> factory)
    : FileTypeSummaryProjectionHandler<FileTypeContentTypeChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeSummaryViewModel?> ApplyEventAsync([NotNull] FileTypeContentTypeChanged baseEvent, FileTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<FileTypeSummaryViewModel?>(null);
        }

        return Task.FromResult<FileTypeSummaryViewModel?>(summary with { ContentType = baseEvent.ContentType });
    }
}