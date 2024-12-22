namespace Hexalith.Documents.Projections.DataExports.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DataExports;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DataExportStartedOnDetailsProjectionHandler(IProjectionFactory<DataExportDetailsViewModel> factory)
    : DataExportDetailsProjectionHandler<DataExportStarted>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportDetailsViewModel?> ApplyEventAsync([NotNull] DataExportStarted baseEvent, DataExportDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DataExportDetailsViewModel?>(new DataExportDetailsViewModel(
            baseEvent.Id,
            0L,
            baseEvent.DateTime,
            null));
    }
}