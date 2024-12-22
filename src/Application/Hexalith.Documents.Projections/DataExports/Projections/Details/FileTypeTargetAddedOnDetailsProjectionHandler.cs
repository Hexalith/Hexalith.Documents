namespace Hexalith.Documents.Projections.DataExports.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a DataExportTargetAdded event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportTargetAddedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportTargetAddedOnDetailsProjectionHandler(IProjectionFactory<DataExportDetailsViewModel> factory) : DataExportDetailsProjectionHandler<DataExportTargetAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportDetailsViewModel?> ApplyEventAsync([NotNull] DataExportTargetAdded baseEvent, DataExportDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DataExportDetailsViewModel?>(null);
        }

        return Task.FromResult<DataExportDetailsViewModel?>(model with
        {
            Targets = model.Targets
                .Append(baseEvent.Target)
                .Distinct()
                .OrderBy(p => p),
        });
    }
}