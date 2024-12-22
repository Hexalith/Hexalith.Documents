namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handles the projection update when a document container is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentContainerEnabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerEnabledOnSummaryProjectionHandler(IProjectionFactory<DocumentContainerSummaryViewModel> factory)
    : DocumentContainerSummaryProjectionHandler<DocumentContainerEnabled>(factory)
{
    /// <summary>
    /// Applies the event to the summary projection.
    /// </summary>
    /// <param name="baseEvent">The event to apply.</param>
    /// <param name="summary">The current summary projection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary projection.</returns>
    protected override Task<DocumentContainerSummaryViewModel?> ApplyEventAsync([NotNull] DocumentContainerEnabled baseEvent, DocumentContainerSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentContainerSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentContainerSummaryViewModel?>(summary with { Disabled = false });
    }
}