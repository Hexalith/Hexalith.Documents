namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Documents.Requests.DocumentPartitions;

/// <summary>
/// Handles the projection update when a document partition is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentPartitionEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentPartitionEnabledOnDetailsProjectionHandler(IProjectionFactory<DocumentPartitionDetailsViewModel> factory)
    : DocumentPartitionDetailsProjectionHandler<DocumentPartitionEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentPartitionDetailsViewModel?> ApplyEventAsync([NotNull] DocumentPartitionEnabled baseEvent, DocumentPartitionDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled == false)
        {
            return Task.FromResult<DocumentPartitionDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentPartitionDetailsViewModel?>(model with { Disabled = false });
    }
}