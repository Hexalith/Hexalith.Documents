namespace Hexalith.Documents.UI.Services.FileTypes.Projections.Collections;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Extensions.Helpers;

/// <summary>
/// Handles the projection updates for the collection of file type IDs when a new file type is added.
/// </summary>
public partial class FileTypeAddedOnIdsCollectionProjectionHandler(
    IProjectionFactory<FileTypeIds> factory)
    : IProjectionUpdateHandler<FileTypeAdded>
{
    /// <inheritdoc/>
    public async Task ApplyAsync(FileTypeAdded baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        FileTypeIds? currentValue = null;
        string pageId = nameof(FileTypeIds);

        // Loop through the pages to find a page with the current file type ID.
        do
        {
            currentValue = await factory
            .GetStateAsync(pageId, cancellationToken)
            .ConfigureAwait(false)
            ?? new FileTypeIds(null, []);

            if (currentValue.Ids.Any(p => p == metadata.AggregateGlobalId))
            {
                // The file type ID is already in the collection.
                return;
            }

            if (currentValue.NextPageId is not null)
            {
                pageId = currentValue.NextPageId;
            }
        }
        while (currentValue.NextPageId is not null);

        // The file type ID is not in the collection. Add it.
        if (currentValue.Ids.Count() > 10000)
        {
            // The collection is full. Create a new page.
            string newPageId = UniqueIdHelper.GenerateUniqueStringId();
            await factory
                .SetStateAsync(
                    pageId,
                    currentValue with { NextPageId = newPageId },
                    cancellationToken)
                .ConfigureAwait(false);
            pageId = newPageId;
            currentValue = new FileTypeIds(null, []);
            return;
        }

        await factory
            .SetStateAsync(
                pageId,
                currentValue with { Ids = currentValue.Ids.Append(metadata.AggregateGlobalId).Distinct().OrderBy(p => p) },
                cancellationToken)
            .ConfigureAwait(false);
    }
}