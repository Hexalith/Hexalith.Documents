namespace Hexalith.Documents.Projections.Documents.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handles the projection update when a document is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentAddedOnSummaryProjectionHandler(IProjectionFactory<DocumentSummaryViewModel> factory)
    : DocumentSummaryProjectionHandler<DocumentAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentSummaryViewModel?> ApplyEventAsync([NotNull] DocumentAdded baseEvent, DocumentSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentSummaryViewModel?>(new DocumentSummaryViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.DocumentContainerId,
            baseEvent.Files.Sum(p => p.Size),
            false));
    }
}