namespace Hexalith.Documents.Projections.Documents.RequestHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handler for getting document details.
/// </summary>
public class GetDocumentDetailsHandler : RequestHandlerBase<GetDocumentDetails>
{
    private readonly IProjectionFactory<DocumentDetailsViewModel> _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentDetailsHandler"/> class.
    /// </summary>
    /// <param name="projectionFactory">The projection factory.</param>
    /// <exception cref="ArgumentNullException">Thrown when projectionFactory is null.</exception>
    public GetDocumentDetailsHandler(IProjectionFactory<DocumentDetailsViewModel> projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _projectionFactory = projectionFactory;
    }

    /// <inheritdoc/>
    public override async Task<GetDocumentDetails> ExecuteAsync(GetDocumentDetails request, Metadata metadata, CancellationToken cancellationToken)
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