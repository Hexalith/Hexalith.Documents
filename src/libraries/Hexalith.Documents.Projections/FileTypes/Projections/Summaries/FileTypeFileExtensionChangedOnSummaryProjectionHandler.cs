// <copyright file="FileTypeFileExtensionChangedOnSummaryProjectionHandler.cs" company="ITANEO">
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
/// Handles the projection update when a file type's file extension is changed.
/// </summary>
public class FileTypeFileExtensionChangedOnSummaryProjectionHandler : FileTypeSummaryProjectionHandler<FileTypeFileExtensionChanged>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeFileExtensionChangedOnSummaryProjectionHandler"/> class.
    /// </summary>
    /// <param name="factory">The projection factory.</param>
    public FileTypeFileExtensionChangedOnSummaryProjectionHandler(IProjectionFactory<FileTypeSummaryViewModel> factory)
        : base(factory)
    {
    }

    /// <inheritdoc/>
    protected override Task<FileTypeSummaryViewModel?> ApplyEventAsync([NotNull] FileTypeFileExtensionChanged baseEvent, FileTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<FileTypeSummaryViewModel?>(null);
        }

        return Task.FromResult<FileTypeSummaryViewModel?>(summary with { FileExtension = baseEvent.FileExtension });
    }
}