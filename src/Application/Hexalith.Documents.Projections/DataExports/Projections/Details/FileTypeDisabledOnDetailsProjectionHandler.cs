namespace Hexalith.Documents.Projections.DataExports.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportDisabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportDisabledOnDetailsProjectionHandler(IProjectionFactory<DataExportDetailsViewModel> factory)
    : DataExportDetailsProjectionHandler<DataExportDisabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportDetailsViewModel?> ApplyEventAsync([NotNull] DataExportDisabled baseEvent, DataExportDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled)
        {
            return Task.FromResult<DataExportDetailsViewModel?>(null);
        }

        return Task.FromResult<DataExportDetailsViewModel?>(model with { Disabled = true });
    }
}