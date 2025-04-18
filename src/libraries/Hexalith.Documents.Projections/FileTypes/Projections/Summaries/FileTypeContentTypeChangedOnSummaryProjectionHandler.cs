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
public class FileTypeContentTypeChangedOnSummaryProjectionHandler : FileTypeSummaryProjectionHandler<FileTypeContentTypeChanged>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeContentTypeChangedOnSummaryProjectionHandler"/> class.
    /// </summary>
    /// <param name="factory">The projection factory.</param>
    public FileTypeContentTypeChangedOnSummaryProjectionHandler(IProjectionFactory<FileTypeSummaryViewModel> factory)
        : base(factory)
    {
    }

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