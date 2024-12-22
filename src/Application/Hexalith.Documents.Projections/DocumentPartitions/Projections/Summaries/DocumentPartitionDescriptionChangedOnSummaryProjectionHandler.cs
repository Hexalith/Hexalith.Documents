namespace Hexalith.Documents.Projections.DocumentPartitions.Projections.Summaries;

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
/// Initializes a new instance of the <see cref="DocumentPartitionDescriptionChangedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentPartitionDescriptionChangedOnSummaryProjectionHandler(IProjectionFactory<DocumentPartitionSummaryViewModel> factory)
    : DocumentPartitionSummaryProjectionHandler<DocumentPartitionDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentPartitionSummaryViewModel?> ApplyEventAsync([NotNull] DocumentPartitionDescriptionChanged baseEvent, DocumentPartitionSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentPartitionSummaryViewModel?>(new DocumentPartitionSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
        }

        return Task.FromResult<DocumentPartitionSummaryViewModel?>(summary with { Name = baseEvent.Name });
    }
}