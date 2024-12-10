namespace Hexalith.Documents.UI.Services.FileTypes.RequestHandlers;

using System;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handler for getting file type IDs.
/// </summary>
public class GetFileTypeIdsHandler : RequestHandlerBase<GetFileTypeIds>
{
    private readonly IIdCollectionFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeIdsHandler"/> class.
    /// </summary>
    /// <param name="factory">The factory to create ID collection services.</param>
    public GetFileTypeIdsHandler(IIdCollectionFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _factory = factory;
    }

    /// <summary>
    /// Executes the request to get file type IDs.
    /// </summary>
    /// <param name="baseRequest">The base request containing parameters.</param>
    /// <param name="metadata">The metadata associated with the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task<GetFileTypeIds> ExecuteAsync(GetFileTypeIds baseRequest, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseRequest);
        ArgumentNullException.ThrowIfNull(metadata);
        IIdCollectionService service = _factory.CreateService(
            IIdCollectionFactory.GetAggregateCollectionName(metadata.Message.Aggregate.Name),
            metadata.Context.PartitionId);
        return await service.GetAsync(CancellationToken.None).ConfigureAwait(false);
    }
}