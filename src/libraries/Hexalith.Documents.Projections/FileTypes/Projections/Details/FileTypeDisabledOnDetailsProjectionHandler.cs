namespace Hexalith.Documents.Projections.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when a file type is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FileTypeDisabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class FileTypeDisabledOnDetailsProjectionHandler(IProjectionFactory<FileTypeDetailsViewModel> factory)
    : FileTypeDetailsProjectionHandler<FileTypeDisabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeDisabled baseEvent, FileTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled)
        {
            return Task.FromResult<FileTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<FileTypeDetailsViewModel?>(model with { Disabled = true });
    }
}