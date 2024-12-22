namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Summaries;

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
/// Initializes a new instance of the <see cref="DocumentPartitionDisabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentPartitionDisabledOnSummaryProjectionHandler(IProjectionFactory<DocumentPartitionSummaryViewModel> factory)
    : DocumentPartitionSummaryProjectionHandler<DocumentPartitionDisabled>(factory)
{
    /// <summary>
    /// Applies the document partition disabled event to the summary view model.
    /// </summary>
    /// <param name="baseEvent">The document partition disabled event.</param>
    /// <param name="summary">The current summary view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary view model.</returns>
    protected override Task<DocumentPartitionSummaryViewModel?> ApplyEventAsync([NotNull] DocumentPartitionDisabled baseEvent, DocumentPartitionSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentPartitionSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentPartitionSummaryViewModel?>(summary with { Disabled = true });
    }
}