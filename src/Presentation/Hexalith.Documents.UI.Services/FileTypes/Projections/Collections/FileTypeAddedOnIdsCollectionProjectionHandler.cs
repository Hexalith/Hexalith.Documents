namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;

/// <summary>
/// Handles the projection updates for the collection of file type IDs when a new file type is added.
/// </summary>
public partial class FileTypeAddedOnIdsCollectionProjectionHandler(
    IProjectionFactory<IEnumerable<string>> factory)
    : IProjectionUpdateHandler<FileTypeAdded>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(FileTypeAdded baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        IEnumerable<string> currentValue = await factory
            .GetStateAsync(DocumentUIConstants.FileTypeIdsCollectionProjectionName, cancellationToken)
            .ConfigureAwait(false)
            ?? [];

        if (currentValue.Any(p => p == metadata.AggregateGlobalId))
        {
            return;
        }

        await factory
            .SetStateAsync(
                metadata.AggregateGlobalId,
                currentValue.Append(metadata.AggregateGlobalId).Distinct().OrderBy(p => p),
                cancellationToken)
            .ConfigureAwait(false);
    }
}