namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Documents.Requests.DocumentPartitions;

/// <summary>
/// Handles the projection update when a document partition is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentPartitionEnabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentPartitionEnabledOnSummaryProjectionHandler(IProjectionFactory<DocumentPartitionSummaryViewModel> factory)
    : DocumentPartitionSummaryProjectionHandler<DocumentPartitionEnabled>(factory)
{
    /// <summary>
    /// Applies the event to the summary projection.
    /// </summary>
    /// <param name="baseEvent">The event to apply.</param>
    /// <param name="summary">The current summary projection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary projection.</returns>
    protected override Task<DocumentPartitionSummaryViewModel?> ApplyEventAsync([NotNull] DocumentPartitionEnabled baseEvent, DocumentPartitionSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentPartitionSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentPartitionSummaryViewModel?>(summary with { Disabled = false });
    }
}