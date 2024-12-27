namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handles the projection update when a document partition is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentStorageEnabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentStorageEnabledOnSummaryProjectionHandler(IProjectionFactory<DocumentStorageSummaryViewModel> factory)
    : DocumentStorageSummaryProjectionHandler<DocumentStorageEnabled>(factory)
{
    /// <summary>
    /// Applies the event to the summary projection.
    /// </summary>
    /// <param name="baseEvent">The event to apply.</param>
    /// <param name="summary">The current summary projection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary projection.</returns>
    protected override Task<DocumentStorageSummaryViewModel?> ApplyEventAsync([NotNull] DocumentStorageEnabled baseEvent, DocumentStorageSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentStorageSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentStorageSummaryViewModel?>(summary with { Disabled = false });
    }
}