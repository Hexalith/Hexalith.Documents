namespace Hexalith.Documents.Projections.DataManagements.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Documents.Requests.DataManagements;

/// <summary>
/// Handles the projection update when a data export is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DataManagementStartedOnDetailsProjectionHandler(IProjectionFactory<DataManagementExportViewModel> factory)
    : DataManagementDetailsProjectionHandler<DataExportStarted>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataManagementExportViewModel?> ApplyEventAsync([NotNull] DataExportStarted baseEvent, DataManagementExportViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DataManagementExportViewModel?>(new DataManagementExportViewModel(
            baseEvent.Id,
            0L,
            null,
            baseEvent.DateTime,
            null));
    }
}