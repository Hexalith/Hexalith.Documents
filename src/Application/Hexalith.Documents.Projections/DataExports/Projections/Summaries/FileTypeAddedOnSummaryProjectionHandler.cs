namespace Hexalith.Documents.Projections.DataExports.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DataExportAddedOnSummaryProjectionHandler(IProjectionFactory<DataExportSummaryViewModel> factory)
    : DataExportSummaryProjectionHandler<DataExportAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportSummaryViewModel?> ApplyEventAsync([NotNull] DataExportAdded baseEvent, DataExportSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DataExportSummaryViewModel?>(new DataExportSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
    }
}