namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.UI.Services.FileTypes.ViewModels;

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
    /// <summary>
    /// Applies the file type disabled event to the summary view model.
    /// </summary>
    /// <param name="baseEvent">The file type disabled event.</param>
    /// <param name="summary">The current summary view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary view model.</returns>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeDisabled baseEvent, FileTypeDetailsViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null || summary.Disabled)
        {
            return Task.FromResult<FileTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<FileTypeDetailsViewModel?>(summary with { Disabled = true });
    }
}