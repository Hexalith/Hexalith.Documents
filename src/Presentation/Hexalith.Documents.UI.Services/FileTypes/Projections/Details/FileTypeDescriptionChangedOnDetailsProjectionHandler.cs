namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Details;

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
/// Initializes a new instance of the <see cref="FileTypeDescriptionChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class FileTypeDescriptionChangedOnDetailsProjectionHandler(IProjectionFactory<FileTypeDetailsViewModel> factory)
    : FileTypeDetailsProjectionHandler<FileTypeDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeDescriptionChanged baseEvent, FileTypeDetailsViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<FileTypeDetailsViewModel?>(new FileTypeDetailsViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.Description,
            null,
            [],
            false));
    }
}