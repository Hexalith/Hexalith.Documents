namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Collections;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;

/// <summary>
/// Handles the projection updates for the collection of file type IDs when a new file type is added.
/// </summary>
public partial class FileTypeAddedOnIdsCollectionProjectionHandler(IProjectionFactory<IdCollection> factory)
    : IdsCollectionProjectionHandler<FileTypeAdded>(factory)
{
    /// <inheritdoc/>
    protected override bool IsRemoveEvent(FileTypeAdded baseEvent) => false;
}