namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Collections;

using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Services;
using Hexalith.Documents.Events.FileTypes;

/// <summary>
/// Handles the projection updates for the collection of file type IDs when a new file type is added.
/// </summary>
public partial class FileTypeAddedOnIdsCollectionProjectionHandler(IIdCollectionFactory factory) : IProjectionUpdateHandler<FileTypeAdded>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(FileTypeAdded baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        IIdCollectionService service = factory.CreateService(
            DocumentUIConstants.FileTypeIdsProjectionName,
            metadata.Context.PartitionId);
        await service.AddAsync(metadata.AggregateGlobalId, cancellationToken).ConfigureAwait(false);
    }
}