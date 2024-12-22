namespace Hexalith.Documents.Projections.DataExports.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DataExportAddedOnDetailsProjectionHandler(IProjectionFactory<DataExportDetailsViewModel> factory)
    : DataExportDetailsProjectionHandler<DataExportAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportDetailsViewModel?> ApplyEventAsync([NotNull] DataExportAdded baseEvent, DataExportDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DataExportDetailsViewModel?>(new DataExportDetailsViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.Description,
            baseEvent.FileToTextConverter,
            baseEvent.Targets,
            false));
    }
}