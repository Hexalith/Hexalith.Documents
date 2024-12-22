namespace Hexalith.Documents.Projections.DataExports.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportEnabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportEnabledOnSummaryProjectionHandler(IProjectionFactory<DataExportSummaryViewModel> factory)
    : DataExportSummaryProjectionHandler<DataExportEnabled>(factory)
{
    /// <summary>
    /// Applies the event to the summary projection.
    /// </summary>
    /// <param name="baseEvent">The event to apply.</param>
    /// <param name="summary">The current summary projection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary projection.</returns>
    protected override Task<DataExportSummaryViewModel?> ApplyEventAsync([NotNull] DataExportEnabled baseEvent, DataExportSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DataExportSummaryViewModel?>(null);
        }

        return Task.FromResult<DataExportSummaryViewModel?>(summary with { Disabled = false });
    }
}