namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handles the projection update when a document container is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentContainerDisabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerDisabledOnSummaryProjectionHandler(IProjectionFactory<DocumentContainerSummaryViewModel> factory)
    : DocumentContainerSummaryProjectionHandler<DocumentContainerDisabled>(factory)
{
    /// <summary>
    /// Applies the document container disabled event to the summary view model.
    /// </summary>
    /// <param name="baseEvent">The document container disabled event.</param>
    /// <param name="summary">The current summary view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary view model.</returns>
    protected override Task<DocumentContainerSummaryViewModel?> ApplyEventAsync([NotNull] DocumentContainerDisabled baseEvent, DocumentContainerSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentContainerSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentContainerSummaryViewModel?>(summary with { Disabled = true });
    }
}