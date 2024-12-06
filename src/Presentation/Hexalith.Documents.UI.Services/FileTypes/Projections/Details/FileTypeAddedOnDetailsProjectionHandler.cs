namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.UI.Services.FileTypes.ViewModels;

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
            baseEvent.Description,
            baseEvent.FileToTextConverter,
            baseEvent.Targets,
            false));
    }
}