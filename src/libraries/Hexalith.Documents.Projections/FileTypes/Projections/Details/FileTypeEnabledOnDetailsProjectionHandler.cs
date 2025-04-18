namespace Hexalith.Documents.Projections.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handles the projection update when a file type is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FileTypeEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class FileTypeEnabledOnDetailsProjectionHandler(IProjectionFactory<FileTypeDetailsViewModel> factory)
    : FileTypeDetailsProjectionHandler<FileTypeEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeEnabled baseEvent, FileTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || !model.Disabled)
        {
            return Task.FromResult<FileTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<FileTypeDetailsViewModel?>(model with { Disabled = false });
    }
}