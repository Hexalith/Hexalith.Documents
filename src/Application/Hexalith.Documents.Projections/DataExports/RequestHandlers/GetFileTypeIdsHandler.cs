namespace Hexalith.Documents.Projections.DataExports.RequestHandlers;

using System;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handler for getting data export IDs.
/// </summary>
public class GetDataExportIdsHandler : RequestHandlerBase<GetDataExportIds>
{
    private readonly IIdCollectionFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataExportIdsHandler"/> class.
    /// </summary>
    /// <param name="factory">The factory to create ID collection services.</param>
    public GetDataExportIdsHandler(IIdCollectionFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _factory = factory;
    }

    /// <summary>
    /// Executes the request to get data export IDs.
    /// </summary>
    /// <param name="request">The base request containing parameters.</param>
    /// <param name="metadata">The metadata associated with the request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task<GetDataExportIds> ExecuteAsync(GetDataExportIds request, Metadata metadata, CancellationToken cancellationToken)
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