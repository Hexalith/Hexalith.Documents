namespace Hexalith.Documents.Projections.FileTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Abstract base class for handling updates to FileType projections based on events.
/// </summary>
/// <typeparam name="TFileTypeEvent">The type of the file type event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class FileTypeSummaryProjectionHandler<TFileTypeEvent>(IProjectionFactory<FileTypeSummaryViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TFileTypeEvent, FileTypeSummaryViewModel>(factory)
    where TFileTypeEvent : FileTypeEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TFileTypeEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        FileTypeSummaryViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        FileTypeSummaryViewModel? newValue = await ApplyEventAsync(
                baseEvent,
                currentValue,
                cancellationToken)
            .ConfigureAwait(false);
        if (newValue == null)
        {
            return;
        }

        await SaveProjectionAsync(metadata.AggregateGlobalId, newValue, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Applies the event to the file type summary view model.
    /// </summary>
    /// <param name="baseEvent">The file type event.</param>
    /// <param name="summary">The existing file type summary view model, if any.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated file type summary view model.</returns>
    protected abstract Task<FileTypeSummaryViewModel?> ApplyEventAsync(TFileTypeEvent baseEvent, FileTypeSummaryViewModel? summary, CancellationToken cancellationToken);
}