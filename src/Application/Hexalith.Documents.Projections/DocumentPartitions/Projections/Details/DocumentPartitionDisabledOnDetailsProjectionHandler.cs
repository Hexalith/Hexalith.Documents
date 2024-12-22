namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Documents.Requests.DocumentPartitions;

/// <summary>
/// Handles the projection update when a document partition is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentPartitionDisabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentPartitionDisabledOnDetailsProjectionHandler(IProjectionFactory<DocumentPartitionDetailsViewModel> factory)
    : DocumentPartitionDetailsProjectionHandler<DocumentPartitionDisabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentPartitionDetailsViewModel?> ApplyEventAsync([NotNull] DocumentPartitionDisabled baseEvent, DocumentPartitionDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled)
        {
            return Task.FromResult<DocumentPartitionDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentPartitionDetailsViewModel?>(model with { Disabled = true });
    }
}