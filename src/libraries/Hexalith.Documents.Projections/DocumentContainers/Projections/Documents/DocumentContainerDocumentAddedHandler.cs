namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Documents;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Events;
using Hexalith.Application.Metadatas;
using Hexalith.Application.Services;
using Hexalith.Documents.DocumentContainers;
using Hexalith.Documents.Documents;
using Hexalith.Documents.Events.Documents;

/// <summary>
/// Handles the projection update when a document container is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentContainerDocumentAddedHandler : IntegrationEventHandlerBase<DocumentAdded>
{
    private readonly IOneToManyAggregateRelationService<DocumentContainer, Document> _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContainerDocumentAddedHandler"/> class.
    /// </summary>
    /// <param name="service">The service.</param>
    public DocumentContainerDocumentAddedHandler(IOneToManyAggregateRelationService<DocumentContainer, Document> service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _service = service;
    }

    /// <inheritdoc/>
    public override async Task<IEnumerable<object>> ApplyAsync(DocumentAdded baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);
        await _service.AddAsync(metadata.Context.PartitionId, baseEvent.DocumentContainerId, baseEvent.Id, cancellationToken).ConfigureAwait(false);
        return [];
    }
}