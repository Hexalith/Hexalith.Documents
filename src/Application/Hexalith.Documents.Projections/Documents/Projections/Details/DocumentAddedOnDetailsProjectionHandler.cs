namespace Hexalith.Documents.Projections.Documents.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handles the projection update when a document is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentDetailsViewModel> factory)
    : DocumentDetailsProjectionHandler<DocumentAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentDetailsViewModel?> ApplyEventAsync([NotNull] DocumentAdded baseEvent, DocumentDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentDetailsViewModel?>(new DocumentDetailsViewModel(
            baseEvent.Id,
            new DocumentDescription(
                baseEvent.Name,
                baseEvent.Comments,
                baseEvent.DocumentContainerId,
                baseEvent.DocumentTypeId,
                null),
            new DocumentRouting(baseEvent.OwnerId, [], []),
            baseEvent.DocumentTypeId,
            DocumentState.Create(baseEvent.CreatedOn, baseEvent.OwnerId),
            [new DocumentActor(baseEvent.OwnerId, DocumentActorRole.Owner)],
            baseEvent.Files,
            [],
            false));
    }
}