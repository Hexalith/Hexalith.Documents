namespace Hexalith.Documents.Projections.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when a FileTypeFileToTextConverterChanged event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FileTypeFileToTextConverterChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class FileTypeFileToTextConverterChangedOnDetailsProjectionHandler(IProjectionFactory<FileTypeDetailsViewModel> factory) : FileTypeDetailsProjectionHandler<FileTypeFileToTextConverterChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeFileToTextConverterChanged baseEvent, FileTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<FileTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<FileTypeDetailsViewModel?>(model with { FileToTextConverter = baseEvent.FileToTextConverter });
    }
}