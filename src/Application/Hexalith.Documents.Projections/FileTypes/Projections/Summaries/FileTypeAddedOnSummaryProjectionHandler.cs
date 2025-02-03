namespace Hexalith.Documents.Projections.FileTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when a file type is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class FileTypeAddedOnSummaryProjectionHandler(IProjectionFactory<FileTypeSummaryViewModel> factory)
    : FileTypeSummaryProjectionHandler<FileTypeAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeSummaryViewModel?> ApplyEventAsync([NotNull] FileTypeAdded baseEvent, FileTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<FileTypeSummaryViewModel?>(new FileTypeSummaryViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.ContentType,
            baseEvent.FileExtension,
            false));
    }
}