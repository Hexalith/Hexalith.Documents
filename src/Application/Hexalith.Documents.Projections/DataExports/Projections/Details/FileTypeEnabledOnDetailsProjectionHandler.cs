namespace Hexalith.Documents.Projections.DataExports.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportEnabledOnDetailsProjectionHandler(IProjectionFactory<DataExportDetailsViewModel> factory)
    : DataExportDetailsProjectionHandler<DataExportEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportDetailsViewModel?> ApplyEventAsync([NotNull] DataExportEnabled baseEvent, DataExportDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled == false)
        {
            return Task.FromResult<DataExportDetailsViewModel?>(null);
        }

        return Task.FromResult<DataExportDetailsViewModel?>(model with { Disabled = false });
    }
}