namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Documents.Requests.DocumentPartitions;

/// <summary>
/// Handles the projection update when a document partition is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentPartitionAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentPartitionDetailsViewModel> factory)
    : DocumentPartitionDetailsProjectionHandler<DocumentPartitionAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentPartitionDetailsViewModel?> ApplyEventAsync([NotNull] DocumentPartitionAdded baseEvent, DocumentPartitionDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentPartitionDetailsViewModel?>(new DocumentPartitionDetailsViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.StorageType,
            baseEvent.Description,
            baseEvent.ConnectionString,
            false));
    }
}