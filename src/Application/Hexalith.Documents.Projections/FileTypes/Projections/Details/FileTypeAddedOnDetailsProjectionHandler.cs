namespace Hexalith.Documents.Projections.FileTypes.Projections.Details;

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
public class FileTypeAddedOnDetailsProjectionHandler(IProjectionFactory<FileTypeDetailsViewModel> factory)
    : FileTypeDetailsProjectionHandler<FileTypeAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeAdded baseEvent, FileTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<FileTypeDetailsViewModel?>(new FileTypeDetailsViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.ContentType,
            baseEvent.OtherContentTypes,
            baseEvent.FileExtension,
            baseEvent.Description,
            baseEvent.FileToTextConverter,
            false));
    }
}