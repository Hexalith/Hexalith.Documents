namespace Hexalith.Documents.Projections.Documents.RequestHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handler for getting document summaries.
/// </summary>
public class GetDocumentSummariesHandler : RequestHandlerBase<GetDocumentSummaries>
{
    private readonly IIdCollectionFactory _collectionFactory;
    private readonly IProjectionFactory<DocumentSummaryViewModel> _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentSummariesHandler"/> class.
    /// </summary>
    /// <param name="collectionFactory">The collection factory.</param>
    /// <param name="projectionFactory">The projection factory.</param>
    public GetDocumentSummariesHandler(IIdCollectionFactory collectionFactory, IProjectionFactory<DocumentSummaryViewModel> projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(collectionFactory);
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _collectionFactory = collectionFactory;
        _projectionFactory = projectionFactory;
    }

    /// <summary>
    /// Executes the request to get document summaries.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="metadata">The metadata.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated request with the result.</returns>
    public override async Task<GetDocumentSummaries> ExecuteAsync(GetDocumentSummaries request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);

        IIdCollectionService service = _collectionFactory.CreateService(
            IIdCollectionFactory.GetAggregateCollectionName(metadata.Message.Aggregate.Name),
            metadata.Context.PartitionId);

        IEnumerable<string> ids = await service
                .GetAsync(request.Skip, request.Take, cancellationToken)
                .ConfigureAwait(false);

        List<Task<DocumentSummaryViewModel?>> summaryTasks = [];

        foreach (string id in ids)
        {
            summaryTasks.Add(_projectionFactory.GetStateAsync(id, cancellationToken));
        }

        DocumentSummaryViewModel?[] results = await Task.WhenAll(summaryTasks).ConfigureAwait(false);

        IEnumerable<DocumentSummaryViewModel> queryResult = results.Where(p => p is not null).OfType<DocumentSummaryViewModel>();

        return request with { Result = queryResult };
    }
}