namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.UI.Services.FileTypes.ViewModels;

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
    /// <summary>
    /// Applies the event to the summary projection.
    /// </summary>
    /// <param name="baseEvent">The event to apply.</param>
    /// <param name="summary">The current summary projection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary projection.</returns>
    protected override Task<FileTypeDetailsViewModel?> ApplyEventAsync([NotNull] FileTypeEnabled baseEvent, FileTypeDetailsViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null || summary.Disabled == false)
        {
            return Task.FromResult<FileTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<FileTypeDetailsViewModel?>(summary with { Disabled = true });
    }
}