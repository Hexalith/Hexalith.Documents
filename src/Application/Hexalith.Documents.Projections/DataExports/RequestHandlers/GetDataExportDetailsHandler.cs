namespace Hexalith.Documents.Projections.DataExports.RequestHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Requests.DataExports;

/// <summary>
/// Handler for getting data export details.
/// </summary>
public class GetDataExportDetailsHandler : RequestHandlerBase<GetDataExportDetails>
{
    private readonly IProjectionFactory<DataExportDetailsViewModel> _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataExportDetailsHandler"/> class.
    /// </summary>
    /// <param name="projectionFactory">The projection factory.</param>
    /// <exception cref="ArgumentNullException">Thrown when projectionFactory is null.</exception>
    public GetDataExportDetailsHandler(IProjectionFactory<DataExportDetailsViewModel> projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _projectionFactory = projectionFactory;
    }

    /// <inheritdoc/>
    public override async Task<GetDataExportDetails> ExecuteAsync(GetDataExportDetails request, Metadata metadata, CancellationToken cancellationToken)
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