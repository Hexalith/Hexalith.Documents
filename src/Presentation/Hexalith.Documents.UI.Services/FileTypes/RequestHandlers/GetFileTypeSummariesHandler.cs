namespace Hexalith.Documents.UI.Services.FileTypes.RequestHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Requests.FileTypes;

/// <summary>
/// Handler for getting file type summaries.
/// </summary>
public class GetFileTypeSummariesHandler : RequestHandlerBase<GetFileTypeSummaries>
{
    private readonly IIdCollectionFactory _collectionFactory;
    private readonly IProjectionFactory<FileTypeSummaryViewModel> _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummariesHandler"/> class.
    /// </summary>
    /// <param name="collectionFactory">The collection factory.</param>
    /// <param name="projectionFactory">The projection factory.</param>
    public GetFileTypeSummariesHandler(IIdCollectionFactory collectionFactory, IProjectionFactory<FileTypeSummaryViewModel> projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(collectionFactory);
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _collectionFactory = collectionFactory;
        _projectionFactory = projectionFactory;
    }

    /// <summary>
    /// Executes the request to get file type summaries.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="metadata">The metadata.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated request with the result.</returns>
    public override async Task<GetFileTypeSummaries> ExecuteAsync(GetFileTypeSummaries request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);

        IIdCollectionService service = _collectionFactory.CreateService(
            IIdCollectionFactory.GetAggregateCollectionName(metadata.Message.Aggregate.Name),
            metadata.Context.PartitionId);

        IEnumerable<string> ids = await service
                .GetAsync(request.Skip, request.Take, cancellationToken)
                .ConfigureAwait(false);

        List<Task<FileTypeSummaryViewModel?>> summaryTasks = [];

        foreach (string id in ids)
        {
            summaryTasks.Add(_projectionFactory.GetStateAsync(id, cancellationToken));
        }

        FileTypeSummaryViewModel?[] results = await Task.WhenAll(summaryTasks).ConfigureAwait(false);

        return request with { Result = results.OfType<FileTypeSummaryViewModel>() };
    }
}