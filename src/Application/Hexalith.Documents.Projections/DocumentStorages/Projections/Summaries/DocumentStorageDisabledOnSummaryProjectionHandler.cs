namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handles the projection update when a document partition is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentStorageDisabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentStorageDisabledOnSummaryProjectionHandler(IProjectionFactory<DocumentStorageSummaryViewModel> factory)
    : DocumentStorageSummaryProjectionHandler<DocumentStorageDisabled>(factory)
{
    /// <summary>
    /// Applies the document partition disabled event to the summary view model.
    /// </summary>
    /// <param name="baseEvent">The document partition disabled event.</param>
    /// <param name="summary">The current summary view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary view model.</returns>
    protected override Task<DocumentStorageSummaryViewModel?> ApplyEventAsync([NotNull] DocumentStorageDisabled baseEvent, DocumentStorageSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentStorageSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentStorageSummaryViewModel?>(summary with { Disabled = true });
    }
}