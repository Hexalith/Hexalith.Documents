namespace Hexalith.Documents.Projections.DataExports.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DataExports;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Abstract base class for handling updates to DataExport projections based on events.
/// </summary>
/// <typeparam name="TDataExportEvent">The type of the data export event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DataExportSummaryProjectionHandler<TDataExportEvent>(IProjectionFactory<DataExportSummaryViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDataExportEvent, DataExportSummaryViewModel>(factory)
    where TDataExportEvent : DataExportEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDataExportEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DataExportSummaryViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DataExportSummaryViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the data export summary view model.
    /// </summary>
    /// <param name="baseEvent">The data export event.</param>
    /// <param name="summary">The existing data export summary view model, if any.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated data export summary view model.</returns>
    protected abstract Task<DataExportSummaryViewModel?> ApplyEventAsync(TDataExportEvent baseEvent, DataExportSummaryViewModel? summary, CancellationToken cancellationToken);
}