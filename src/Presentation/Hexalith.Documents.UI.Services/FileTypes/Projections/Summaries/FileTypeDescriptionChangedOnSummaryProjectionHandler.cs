namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.UI.Services.FileTypes.ViewModels;

/// <summary>
/// Handles the projection update when a file type description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FileTypeDescriptionChangedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class FileTypeDescriptionChangedOnSummaryProjectionHandler(IProjectionFactory<FileTypeSummaryViewModel> factory)
    : FileTypeSummaryProjectionHandler<FileTypeDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeSummaryViewModel?> ApplyEventAsync([NotNull] FileTypeDescriptionChanged baseEvent, FileTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<FileTypeSummaryViewModel?>(new FileTypeSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
        }

        return Task.FromResult<FileTypeSummaryViewModel?>(summary with { Name = baseEvent.Name });
    }
}