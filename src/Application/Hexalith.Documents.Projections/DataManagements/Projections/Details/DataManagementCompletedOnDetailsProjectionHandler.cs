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
public class DataManagementCompletedOnDetailsProjectionHandler(IProjectionFactory<DataManagementDetailsViewModel> factory)
    : DataManagementDetailsProjectionHandler<DataExportCompleted>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataManagementDetailsViewModel?> ApplyEventAsync([NotNull] DataExportCompleted baseEvent, DataManagementDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DataManagementDetailsViewModel?>(null);
        }

        return Task.FromResult<DataManagementDetailsViewModel?>(model with { Size = baseEvent.Size, CompletedAt = baseEvent.DateTime });
    }
}