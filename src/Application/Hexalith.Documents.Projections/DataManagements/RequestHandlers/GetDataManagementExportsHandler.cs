namespace Hexalith.Documents.Projections.DataManagements.RequestHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Domain.DataManagements;
using Hexalith.Documents.Requests.DataManagements;
using Hexalith.Domain.Events;

/// <summary>
/// Handles requests to retrieve data management exports.
/// </summary>
/// <remarks>
/// This handler processes requests to fetch data management exports using a combination of
/// aggregate services, collection factories, and projection factories to build the response.
/// It supports pagination through skip and take parameters.
/// </remarks>
public class GetDataManagementExportsHandler : RequestHandlerBase<GetDataManagementExports>
{
    private readonly IAggregateService _aggregateService;
    private readonly IIdCollectionFactory _collectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataManagementExportsHandler"/> class.
    /// </summary>
    /// <param name="aggregateService">The service for managing aggregates in the system.</param>
    /// <param name="collectionFactory">The factory for creating ID collections.</param>
    /// <exception cref="ArgumentNullException">Thrown when any of the parameters is null.</exception>
    public GetDataManagementExportsHandler(IAggregateService aggregateService, IIdCollectionFactory collectionFactory)
    {
        ArgumentNullException.ThrowIfNull(aggregateService);
        ArgumentNullException.ThrowIfNull(collectionFactory);
        _aggregateService = aggregateService;
        _collectionFactory = collectionFactory;
    }

    /// <summary>
    /// Executes the request to retrieve data management exports.
    /// </summary>
    /// <param name="request">The request containing pagination parameters (skip and take).</param>
    /// <param name="metadata">The metadata containing context and aggregate information.</param>
    /// <param name="cancellationToken">A token that can be used to request cancellation of the operation.</param>
    /// <returns>The updated request containing the collection of data management details view models.</returns>
    /// <exception cref="ArgumentNullException">Thrown when request or metadata is null.</exception>
    /// <remarks>
    /// The method performs the following steps:
    /// 1. Validates input parameters
    /// 2. Creates a collection service for the specified aggregate
    /// 3. Retrieves IDs based on pagination parameters
    /// 4. Asynchronously fetches summary view models for each ID
    /// 5. Filters out null results and returns the collection.
    /// </remarks>
    public override async Task<GetDataManagementExports> ExecuteAsync(GetDataManagementExports request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);
        string aggregateName = metadata.Message.Aggregate.Name;
        string partitionId = metadata.Context.PartitionId;
        IIdCollectionService service = _collectionFactory.CreateService(
            IIdCollectionFactory.GetAggregateCollectionName(aggregateName),
            metadata.Context.PartitionId);

        IEnumerable<string> ids = await service
                .GetAsync(request.Skip, request.Take, cancellationToken)
                .ConfigureAwait(false);

        List<Task<SnapshotEvent?>> summaryTasks = [];

        foreach (string id in ids)
        {
            summaryTasks.Add(_aggregateService.GetSnapshotAsync(aggregateName, partitionId, id));
        }

        SnapshotEvent?[] results = await Task.WhenAll(summaryTasks).ConfigureAwait(false);

        IEnumerable<DataManagementExportViewModel> queryResult = results
            .Where(p => p is not null)
            .Select(p => DataManagementExportViewModel.FromAggregate(p.GetAggregate<DataManagement>()))
            .ToList();

        return request with { Results = queryResult };
    }
}