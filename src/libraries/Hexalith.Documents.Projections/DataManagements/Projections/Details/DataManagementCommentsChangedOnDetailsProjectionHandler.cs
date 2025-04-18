namespace Hexalith.Documents.Projections.DataManagements.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Documents.Requests.DataManagements;

public class DataManagementCommentsChangedOnDetailsProjectionHandler(IProjectionFactory<DataManagementDetailsViewModel> factory)
    : DataManagementDetailsProjectionHandler<DataManagementCommentsChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DataManagementDetailsViewModel?> ApplyEventAsync(
        [NotNull] DataManagementCommentsChanged baseEvent,
        DataManagementDetailsViewModel? model,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DataManagementDetailsViewModel?>(null);
        }

        return Task.FromResult<DataManagementDetailsViewModel?>(model with { Comments = baseEvent.Comments });
    }
}