namespace Hexalith.Documents.Projections.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when a file type's file extension is changed.
/// </summary>
public class FileTypeFileExtensionChangedOnDetailsProjectionHandler : FileTypeDetailsProjectionHandler<FileTypeFileExtensionChanged>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeFileExtensionChangedOnDetailsProjectionHandler"/> class.
    /// </summary>
    /// <param name="factory">The projection factory.</param>
    public FileTypeFileExtensionChangedOnDetailsProjectionHandler(IProjectionFactory<FileTypeDetailsViewModel> factory)
        : base(factory)
    {
    }

    /// <inheritdoc/>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeFileExtensionChanged baseEvent, FileTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<FileTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<FileTypeDetailsViewModel?>(model with { FileExtension = baseEvent.FileExtension });
    }
}