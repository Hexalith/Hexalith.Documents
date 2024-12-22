namespace Hexalith.Documents.Projections.DataExports.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportDisabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportDisabledOnSummaryProjectionHandler(IProjectionFactory<DataExportSummaryViewModel> factory)
    : DataExportSummaryProjectionHandler<DataExportDisabled>(factory)
{
    /// <summary>
    /// Applies the data export disabled event to the summary view model.
    /// </summary>
    /// <param name="baseEvent">The data export disabled event.</param>
    /// <param name="summary">The current summary view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary view model.</returns>
    protected override Task<DataExportSummaryViewModel?> ApplyEventAsync([NotNull] DataExportDisabled baseEvent, DataExportSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DataExportSummaryViewModel?>(null);
        }

        return Task.FromResult<DataExportSummaryViewModel?>(summary with { Disabled = true });
    }
}