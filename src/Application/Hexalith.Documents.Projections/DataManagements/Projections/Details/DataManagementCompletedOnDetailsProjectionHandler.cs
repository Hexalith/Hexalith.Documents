namespace Hexalith.Documents.Projections.DataManagements.Projections.Details;

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
/// Initializes a new instance of the <see cref="DataManagementCompletedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DataManagementCompletedOnDetailsProjectionHandler(IProjectionFactory<DataManagementExportViewModel> factory)
    : DataManagementDetailsProjectionHandler<DataExportCompleted>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataManagementExportViewModel?> ApplyEventAsync([NotNull] DataExportCompleted baseEvent, DataManagementExportViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DataManagementExportViewModel?>(null);
        }

        return Task.FromResult<DataManagementExportViewModel?>(model with { Size = baseEvent.Size, CompletedAt = baseEvent.DateTime });
    }
}