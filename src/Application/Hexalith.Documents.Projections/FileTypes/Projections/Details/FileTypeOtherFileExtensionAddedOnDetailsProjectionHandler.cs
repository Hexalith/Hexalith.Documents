namespace Hexalith.Documents.Projections.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when a FileTypeTargetAdded event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FileTypeOtherFileExtensionAddedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class FileTypeOtherFileExtensionAddedOnDetailsProjectionHandler(IProjectionFactory<FileTypeDetailsViewModel> factory) : FileTypeDetailsProjectionHandler<FileTypeOtherFileExtensionAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeOtherFileExtensionAdded baseEvent, FileTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<FileTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<FileTypeDetailsViewModel?>(model with
        {
            OtherFileExtensions = model.OtherFileExtensions
                .Append(baseEvent.OtherFileExtension)
                .Distinct()
                .OrderBy(p => p),
        });
    }
}