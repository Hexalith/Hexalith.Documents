namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Documents.Requests.DocumentPartitions;

/// <summary>
/// Handles the projection update when a document partition is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentPartitionAddedOnSummaryProjectionHandler(IProjectionFactory<DocumentPartitionSummaryViewModel> factory)
    : DocumentPartitionSummaryProjectionHandler<DocumentPartitionAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentPartitionSummaryViewModel?> ApplyEventAsync([NotNull] DocumentPartitionAdded baseEvent, DocumentPartitionSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentPartitionSummaryViewModel?>(new DocumentPartitionSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
    }
}