namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Abstract base class for handling updates to DocumentType projections based on events.
/// </summary>
/// <typeparam name="TDocumentTypeEvent">The type of the document type event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
public abstract class DocumentTypeDetailsProjectionHandler<TDocumentTypeEvent>(IProjectionFactory<DocumentTypeDetailsViewModel> factory)
    : KeyValueProjectionUpdateEventHandlerBase<TDocumentTypeEvent, DocumentTypeDetailsViewModel>(factory)
    where TDocumentTypeEvent : DocumentTypeEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TDocumentTypeEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        DocumentTypeDetailsViewModel? currentValue = await GetProjectionAsync(metadata.AggregateGlobalId, cancellationToken)
            .ConfigureAwait(false);

        DocumentTypeDetailsViewModel? newValue = await ApplyEventAsync(
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
    /// Applies the event to the document type summary view model.
    /// </summary>
    /// <param name="baseEvent">The document type event.</param>
    /// <param name="model">The current document type detail view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated document type summary view model.</returns>
    protected abstract Task<DocumentTypeDetailsViewModel?> ApplyEventAsync(TDocumentTypeEvent baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken);
}