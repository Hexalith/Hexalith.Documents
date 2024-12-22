namespace Hexalith.Documents.Projections.DocumentContainers.RequestHandlers;

using System;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handler for getting document container IDs.
/// </summary>
public class GetDocumentContainerIdsHandler : RequestHandlerBase<GetDocumentContainerIds>
{
    private readonly IIdCollectionFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerIdsHandler"/> class.
    /// </summary>
    /// <param name="factory">The factory to create ID collection services.</param>
    public GetDocumentContainerIdsHandler(IIdCollectionFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _factory = factory;
    }

    /// <summary>
    /// Executes the request to get document container IDs.
    /// </summary>
    /// <param name="request">The base request containing parameters.</param>
    /// <param name="metadata">The metadata associated with the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task<GetDocumentContainerIds> ExecuteAsync(GetDocumentContainerIds request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);
        IIdCollectionService service = _factory.CreateService(
            IIdCollectionFactory.GetAggregateCollectionName(metadata.Message.Aggregate.Name),
            metadata.Context.PartitionId);
        return request with
        {
            Result = await service
                .GetAsync(request.Skip, request.Take, CancellationToken.None)
                .ConfigureAwait(false),
        };
    }
}