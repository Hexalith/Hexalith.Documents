namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Abstract base class for handling updates to DocumentContainer projections based on events.
/// </summary>
/// <typeparam name="TDocumentContainerEvent">The type of the document container event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentContainerDetailsProjectionHandler<TDocumentContainerEvent>(IProjectionFactory<DocumentContainerDetailsViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentContainerEvent, DocumentContainerDetailsViewModel>(factory)
    where TDocumentContainerEvent : DocumentContainerEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentContainerEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentContainerDetailsViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentContainerDetailsViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the document container summary view model.
    /// </summary>
    /// <param name="baseEvent">The document container event.</param>
    /// <param name="model">The current document container detail view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document container summary view model.</returns>
    protected abstract Task<DocumentContainerDetailsViewModel?> ApplyEventAsync(TDocumentContainerEvent baseEvent, DocumentContainerDetailsViewModel? model, CancellationToken cancellationToken);
}