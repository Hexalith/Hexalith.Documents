namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a document type is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentTypeAddedOnSummaryProjectionHandler(IProjectionFactory<DocumentTypeSummaryViewModel> factory)
    : DocumentTypeSummaryProjectionHandler<DocumentTypeAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeSummaryViewModel?> ApplyEventAsync([NotNull] DocumentTypeAdded baseEvent, DocumentTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentTypeSummaryViewModel?>(new DocumentTypeSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
    }
}