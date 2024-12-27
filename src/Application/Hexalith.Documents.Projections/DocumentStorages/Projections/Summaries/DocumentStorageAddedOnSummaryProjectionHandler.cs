namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handles the projection update when a document partition is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentStorageAddedOnSummaryProjectionHandler(IProjectionFactory<DocumentStorageSummaryViewModel> factory)
    : DocumentStorageSummaryProjectionHandler<DocumentStorageAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentStorageSummaryViewModel?> ApplyEventAsync([NotNull] DocumentStorageAdded baseEvent, DocumentStorageSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentStorageSummaryViewModel?>(new DocumentStorageSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
    }
}