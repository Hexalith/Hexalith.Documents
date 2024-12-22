namespace Hexalith.Documents.Projections.DataExports.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a DataExportTargetRemoved event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportTargetRemovedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportTargetRemovedOnDetailsProjectionHandler(IProjectionFactory<DataExportDetailsViewModel> factory) : DataExportDetailsProjectionHandler<DataExportTargetRemoved>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportDetailsViewModel?> ApplyEventAsync([NotNull] DataExportTargetRemoved baseEvent, DataExportDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DataExportDetailsViewModel?>(null);
        }

        return Task.FromResult<DataExportDetailsViewModel?>(model with
        {
            Targets = model.Targets.Where(p => p != baseEvent.Target),
        });
    }
}