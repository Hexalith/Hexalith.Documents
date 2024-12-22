namespace Hexalith.Documents.Projections.DocumentInformationExtractions.RequestHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Handler for getting document information extraction details.
/// </summary>
public class GetDocumentInformationExtractionDetailsHandler : RequestHandlerBase<GetDocumentInformationExtractionDetails>
{
    private readonly IProjectionFactory<DocumentInformationExtractionDetailsViewModel> _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionDetailsHandler"/> class.
    /// </summary>
    /// <param name="projectionFactory">The projection factory.</param>
    /// <exception cref="ArgumentNullException">Thrown when projectionFactory is null.</exception>
    public GetDocumentInformationExtractionDetailsHandler(IProjectionFactory<DocumentInformationExtractionDetailsViewModel> projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _projectionFactory = projectionFactory;
    }

    /// <inheritdoc/>
    public override async Task<GetDocumentInformationExtractionDetails> ExecuteAsync(GetDocumentInformationExtractionDetails request, Metadata metadata, CancellationToken cancellationToken)
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