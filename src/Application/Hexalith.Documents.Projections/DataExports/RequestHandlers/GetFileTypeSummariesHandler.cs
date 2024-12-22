namespace Hexalith.Documents.Projections.DataExports.RequestHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handler for getting data export summaries.
/// </summary>
public class GetDataExportSummariesHandler : RequestHandlerBase<GetDataExportSummaries>
{
    private readonly IIdCollectionFactory _collectionFactory;
    private readonly IProjectionFactory<DataExportSummaryViewModel> _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataExportSummariesHandler"/> class.
    /// </summary>
    /// <param name="collectionFactory">The collection factory.</param>
    /// <param name="projectionFactory">The projection factory.</param>
    public GetDataExportSummariesHandler(IIdCollectionFactory collectionFactory, IProjectionFactory<DataExportSummaryViewModel> projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(collectionFactory);
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _collectionFactory = collectionFactory;
        _projectionFactory = projectionFactory;
    }

    /// <summary>
    /// Executes the request to get data export summaries.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="metadata">The metadata.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated request with the result.</returns>
    public override async Task<GetDataExportSummaries> ExecuteAsync(GetDataExportSummaries request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);

        IIdCollectionService service = _collectionFactory.CreateService(
            IIdCollectionFactory.GetAggregateCollectionName(metadata.Message.Aggregate.Name),
            metadata.Context.PartitionId);

        IEnumerable<string> ids = await service
                .GetAsync(request.Skip, request.Take, cancellationToken)
                .ConfigureAwait(false);

        List<Task<DataExportSummaryViewModel?>> summaryTasks = [];

        foreach (string id in ids)
        {
            summaryTasks.Add(_projectionFactory.GetStateAsync(id, cancellationToken));
        }

        DataExportSummaryViewModel?[] results = await Task.WhenAll(summaryTasks).ConfigureAwait(false);

        IEnumerable<DataExportSummaryViewModel> queryResult = results.Where(p => p is not null).OfType<DataExportSummaryViewModel>();

        return request with { Result = queryResult };
    }
}