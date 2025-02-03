namespace Hexalith.Documents.Projections.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when a FileTypeTargetRemoved event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FileTypeOtherContentTypeRemovedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class FileTypeOtherContentTypeRemovedOnDetailsProjectionHandler(IProjectionFactory<FileTypeDetailsViewModel> factory) : FileTypeDetailsProjectionHandler<FileTypeOtherContentTypeRemoved>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeOtherContentTypeRemoved baseEvent, FileTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<FileTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<FileTypeDetailsViewModel?>(model with
        {
            OtherContentTypes = model.OtherContentTypes.Where(p => p != baseEvent.OtherContentType),
        });
    }
}