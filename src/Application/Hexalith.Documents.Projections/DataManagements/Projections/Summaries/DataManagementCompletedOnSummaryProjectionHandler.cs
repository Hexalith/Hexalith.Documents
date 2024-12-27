namespace Hexalith.Documents.Projections.DataManagements.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Documents.Requests.DataManagements;

/// <summary>
/// Handles the projection update when a data export description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataManagementCompletedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataManagementCompletedOnSummaryProjectionHandler(IProjectionFactory<DataManagementSummaryViewModel> factory)
    : DataManagementSummaryProjectionHandler<DataExportCompleted>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataManagementSummaryViewModel?> ApplyEventAsync([NotNull] DataExportCompleted baseEvent, DataManagementSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DataManagementSummaryViewModel?>(null);
        }

        return Task.FromResult<DataManagementSummaryViewModel?>(summary with { Size = baseEvent.Size, CompletedAt = baseEvent.DateTime });
    }
}