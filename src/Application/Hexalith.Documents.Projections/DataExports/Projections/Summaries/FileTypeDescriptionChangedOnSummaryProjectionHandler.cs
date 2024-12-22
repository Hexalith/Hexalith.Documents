namespace Hexalith.Documents.Projections.DataExports.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportDescriptionChangedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportDescriptionChangedOnSummaryProjectionHandler(IProjectionFactory<DataExportSummaryViewModel> factory)
    : DataExportSummaryProjectionHandler<DataExportDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportSummaryViewModel?> ApplyEventAsync([NotNull] DataExportDescriptionChanged baseEvent, DataExportSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DataExportSummaryViewModel?>(new DataExportSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
        }

        return Task.FromResult<DataExportSummaryViewModel?>(summary with { Name = baseEvent.Name });
    }
}