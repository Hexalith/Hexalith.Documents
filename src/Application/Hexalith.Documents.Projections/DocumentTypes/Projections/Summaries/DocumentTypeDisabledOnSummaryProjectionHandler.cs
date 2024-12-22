namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a document type is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTypeDisabledOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTypeDisabledOnSummaryProjectionHandler(IProjectionFactory<DocumentTypeSummaryViewModel> factory)
    : DocumentTypeSummaryProjectionHandler<DocumentTypeDisabled>(factory)
{
    /// <summary>
    /// Applies the document type disabled event to the summary view model.
    /// </summary>
    /// <param name="baseEvent">The document type disabled event.</param>
    /// <param name="summary">The current summary view model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated summary view model.</returns>
    protected override Task<DocumentTypeSummaryViewModel?> ApplyEventAsync([NotNull] DocumentTypeDisabled baseEvent, DocumentTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentTypeSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentTypeSummaryViewModel?>(summary with { Disabled = true });
    }
}