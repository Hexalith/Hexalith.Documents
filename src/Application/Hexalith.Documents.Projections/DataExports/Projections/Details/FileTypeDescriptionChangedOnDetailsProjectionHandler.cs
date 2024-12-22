namespace Hexalith.Documents.Projections.DataExports.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a data export description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportDescriptionChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportDescriptionChangedOnDetailsProjectionHandler(IProjectionFactory<DataExportDetailsViewModel> factory)
    : DataExportDetailsProjectionHandler<DataExportDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportDetailsViewModel?> ApplyEventAsync([NotNull] DataExportDescriptionChanged baseEvent, DataExportDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DataExportDetailsViewModel?>(new DataExportDetailsViewModel(
                baseEvent.Id,
                baseEvent.Name,
                baseEvent.Description,
                null,
                [],
                false));
        }

        return Task.FromResult<DataExportDetailsViewModel?>(model with { Name = baseEvent.Name, Description = baseEvent.Description });
    }
}