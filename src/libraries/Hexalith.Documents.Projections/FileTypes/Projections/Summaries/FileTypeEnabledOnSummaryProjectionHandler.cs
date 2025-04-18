namespace Hexalith.Documents.Projections.FileTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when a file type is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FileTypeEnabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class FileTypeEnabledOnSummaryProjectionHandler(IProjectionFactory<FileTypeSummaryViewModel> factory)
    : FileTypeSummaryProjectionHandler<FileTypeEnabled>(factory)
{
    /// <summary>
    /// Applies the event to the summary projection.
    /// </summary>
    /// <param name="baseEvent">The event to apply.</param>
    /// <param name="summary">The current summary projection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary projection.</returns>
    protected override Task<FileTypeSummaryViewModel?> ApplyEventAsync([NotNull] FileTypeEnabled baseEvent, FileTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null || !summary.Disabled)
        {
            return Task.FromResult<FileTypeSummaryViewModel?>(null);
        }

        return Task.FromResult<FileTypeSummaryViewModel?>(summary with { Disabled = false });
    }
}