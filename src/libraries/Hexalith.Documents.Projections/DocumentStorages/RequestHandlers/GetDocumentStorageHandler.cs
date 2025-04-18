namespace Hexalith.Documents.Projections.DocumentStorages.RequestHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Domain.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Domain.Events;

/// <summary>
/// Handler for getting document partition details.
/// </summary>
public class GetDocumentStorageHandler : RequestHandlerBase<GetDocumentStorage>
{
    private readonly IAggregateService _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageHandler"/> class.
    /// </summary>
    /// <param name="projectionFactory">The projection factory.</param>
    /// <exception cref="ArgumentNullException">Thrown when projectionFactory is null.</exception>
    public GetDocumentStorageHandler(IAggregateService projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _projectionFactory = projectionFactory;
    }

    /// <inheritdoc/>
    public override async Task<GetDocumentStorage> ExecuteAsync(GetDocumentStorage request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);

        SnapshotEvent? e = await _projectionFactory
            .GetSnapshotAsync(
                metadata.Message.Aggregate.Name,
                metadata.AggregateGlobalId,
                cancellationToken).ConfigureAwait(false);
        if (e is null || string.IsNullOrWhiteSpace(e.Snapshot))
        {
            return request with { Result = null };
        }

        return request with
        {
            Result = e.GetAggregate<DocumentStorage>(),
        };
    }
}