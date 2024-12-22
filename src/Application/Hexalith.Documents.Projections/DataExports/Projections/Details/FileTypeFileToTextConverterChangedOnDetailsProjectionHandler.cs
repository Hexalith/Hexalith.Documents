namespace Hexalith.Documents.Projections.DataExports.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handles the projection update when a DataExportFileToTextConverterChanged event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataExportFileToTextConverterChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataExportFileToTextConverterChangedOnDetailsProjectionHandler(IProjectionFactory<DataExportDetailsViewModel> factory) : DataExportDetailsProjectionHandler<DataExportFileToTextConverterChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataExportDetailsViewModel?> ApplyEventAsync([NotNull] DataExportFileToTextConverterChanged baseEvent, DataExportDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DataExportDetailsViewModel?>(null);
        }

        return Task.FromResult<DataExportDetailsViewModel?>(model with { FileToTextConverter = baseEvent.FileToTextConverter });
    }
}