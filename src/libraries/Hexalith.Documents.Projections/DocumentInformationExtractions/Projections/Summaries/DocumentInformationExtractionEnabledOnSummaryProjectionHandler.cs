namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Handles the projection update when a document information extraction is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentInformationExtractionEnabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentInformationExtractionEnabledOnSummaryProjectionHandler(IProjectionFactory<DocumentInformationExtractionSummaryViewModel> factory)
    : DocumentInformationExtractionSummaryProjectionHandler<DocumentInformationExtractionEnabled>(factory)
{
    /// <summary>
    /// Applies the event to the summary projection.
    /// </summary>
    /// <param name="baseEvent">The event to apply.</param>
    /// <param name="summary">The current summary projection.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary projection.</returns>
    protected override Task<DocumentInformationExtractionSummaryViewModel?> ApplyEventAsync([NotNull] DocumentInformationExtractionEnabled baseEvent, DocumentInformationExtractionSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentInformationExtractionSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentInformationExtractionSummaryViewModel?>(summary with { Disabled = false });
    }
}