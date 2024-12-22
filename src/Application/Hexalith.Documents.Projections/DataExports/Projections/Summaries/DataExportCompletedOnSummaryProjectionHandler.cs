namespace Hexalith.Documents.Projections.DataExports.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DataExports;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportCompletedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportCompletedOnSummaryProjectionHandler(IProjectionFactory<DataExportSummaryViewModel> factory)
    : DataExportSummaryProjectionHandler<DataExportCompleted>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportSummaryViewModel?> ApplyEventAsync([NotNull] DataExportCompleted baseEvent, DataExportSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DataExportSummaryViewModel?>(null);
        }

        return Task.FromResult<DataExportSummaryViewModel?>(summary with { Size = baseEvent.Size, CompletedAt = baseEvent.DateTime });
    }
}