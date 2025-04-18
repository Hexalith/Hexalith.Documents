namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handles the projection update when a document container is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentContainerCreatedOnSummaryProjectionHandler(IProjectionFactory<DocumentContainerSummaryViewModel> factory)
    : DocumentContainerSummaryProjectionHandler<DocumentContainerCreated>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerSummaryViewModel?> ApplyEventAsync([NotNull] DocumentContainerCreated baseEvent, DocumentContainerSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentContainerSummaryViewModel?>(new DocumentContainerSummaryViewModel(
            baseEvent.Id,
            baseEvent.DocumentStorageId,
            baseEvent.Name,
            false));
    }
}