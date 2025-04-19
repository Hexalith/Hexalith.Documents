namespace Hexalith.Documents.Projections.Documents.RequestHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.DocumentContainers;
using Hexalith.Documents.Documents;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handler for getting document details.
/// </summary>
public class GetDocumentsInContainerHandler : RequestHandlerBase<GetDocumentsInContainer>
{
    private readonly IOneToManyAggregateRelationService<DocumentContainer, Document> _documentsInContainerService;
    private readonly IRequestProcessor _requestProcessor;
    private readonly TimeProvider _timeProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentsInContainerHandler"/> class.
    /// </summary>
    /// <param name="documentsInContainerService">The documents in container service.</param>
    /// <param name="requestProcessor">The request processor.</param>
    /// <param name="timeProvider">The time provider.</param>
    /// <exception cref="ArgumentNullException">Thrown when projectionFactory is null.</exception>
    public GetDocumentsInContainerHandler(
        IOneToManyAggregateRelationService<DocumentContainer, Document> documentsInContainerService,
        IRequestProcessor requestProcessor,
        TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(documentsInContainerService);
        ArgumentNullException.ThrowIfNull(requestProcessor);
        ArgumentNullException.ThrowIfNull(timeProvider);
        _documentsInContainerService = documentsInContainerService;
        _requestProcessor = requestProcessor;
        _timeProvider = timeProvider;
    }

    /// <inheritdoc/>
    public override async Task<GetDocumentsInContainer> ExecuteAsync(GetDocumentsInContainer request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);

        IEnumerable<string> documentIds = await _documentsInContainerService
            .GetAsync(metadata.Context.PartitionId, request.DocumentContainerId, request.Skip, request.Take, cancellationToken)
            .ConfigureAwait(false);
        GetDocumentSummaries? summariesRequest = null;
        if (documentIds.Any())
        {
            summariesRequest = new(documentIds);
            summariesRequest = await _requestProcessor
                .ProcessAsync(
                    summariesRequest,
                    Metadata.CreateNew(summariesRequest, metadata, _timeProvider.GetLocalNow()),
                    cancellationToken)
                .ConfigureAwait(false) as GetDocumentSummaries;
        }

        return request with
        {
            Results = summariesRequest is null ? [] : [.. summariesRequest.Results],
        };
    }
}