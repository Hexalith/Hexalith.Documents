namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handles the projection update when a document partition is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentStorageAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentStorageDetailsViewModel> factory)
    : DocumentStorageDetailsProjectionHandler<DocumentStorageAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentStorageDetailsViewModel?> ApplyEventAsync([NotNull] DocumentStorageAdded baseEvent, DocumentStorageDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentStorageDetailsViewModel?>(new DocumentStorageDetailsViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.StorageType,
            baseEvent.Description,
            baseEvent.ConnectionString,
            false));
    }
}