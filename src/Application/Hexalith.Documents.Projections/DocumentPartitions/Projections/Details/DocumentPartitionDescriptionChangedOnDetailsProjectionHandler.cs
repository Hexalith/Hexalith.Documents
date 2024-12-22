namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Documents.Requests.DocumentPartitions;

/// <summary>
/// Handles the projection update when a document partition description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentPartitionDescriptionChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentPartitionDescriptionChangedOnDetailsProjectionHandler(IProjectionFactory<DocumentPartitionDetailsViewModel> factory)
    : DocumentPartitionDetailsProjectionHandler<DocumentPartitionDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentPartitionDetailsViewModel?> ApplyEventAsync([NotNull] DocumentPartitionDescriptionChanged baseEvent, DocumentPartitionDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentPartitionDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentPartitionDetailsViewModel?>(model with { Name = baseEvent.Name, Description = baseEvent.Description });
    }
}