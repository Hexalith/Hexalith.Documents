namespace Hexalith.Documents.Projections.DocumentPartitions.RequestHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handler for getting document partition details.
/// </summary>
public class GetDocumentStorageHandler : RequestHandlerBase<GetDocumentStorage>
{
    private readonly IProjectionFactory<DocumentStorage> _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageHandler"/> class.
    /// </summary>
    /// <param name="projectionFactory">The projection factory.</param>
    /// <exception cref="ArgumentNullException">Thrown when projectionFactory is null.</exception>
    public GetDocumentStorageHandler(IProjectionFactory<DocumentStorage> projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _projectionFactory = projectionFactory;
    }

    /// <inheritdoc/>
    public override async Task<GetDocumentStorage> ExecuteAsync(GetDocumentStorage request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);

        return request with
        {
            Result = await _projectionFactory
                .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
                .ConfigureAwait(false)
                    ?? throw new InvalidOperationException($"File type {metadata.AggregateGlobalId} not found."),
        };
    }
}