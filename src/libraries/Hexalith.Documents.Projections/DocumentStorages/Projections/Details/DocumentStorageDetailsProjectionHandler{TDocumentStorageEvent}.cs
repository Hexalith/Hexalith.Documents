namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Abstract base class for handling updates to DocumentStorage projections based on events.
/// </summary>
/// <typeparam name="TDocumentStorageEvent">The type of the document partition event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentStorageDetailsProjectionHandler<TDocumentStorageEvent>(IProjectionFactory<DocumentStorageDetailsViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentStorageEvent, DocumentStorageDetailsViewModel>(factory)
    where TDocumentStorageEvent : DocumentStorageEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentStorageEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentStorageDetailsViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentStorageDetailsViewModel? newValue = await ApplyEventAsync(
                baseEvent,
                currentValue,
                cancellationToken)
            .ConfigureAwait(false);
        if (newValue == null || newValue == currentValue)
        {
            return;
        }

        await SaveProjectionAsync(metadata.AggregateGlobalId, newValue, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Applies the event to the document partition summary view model.
    /// </summary>
    /// <param name="baseEvent">The document partition event.</param>
    /// <param name="model">The current document partition detail view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document partition summary view model.</returns>
    protected abstract Task<DocumentStorageDetailsViewModel?> ApplyEventAsync(TDocumentStorageEvent baseEvent, DocumentStorageDetailsViewModel? model, CancellationToken cancellationToken);
}