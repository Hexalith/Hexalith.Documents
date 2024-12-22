namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a document type description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTypeDescriptionChangedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTypeDescriptionChangedOnSummaryProjectionHandler(IProjectionFactory<DocumentTypeSummaryViewModel> factory)
    : DocumentTypeSummaryProjectionHandler<DocumentTypeDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeSummaryViewModel?> ApplyEventAsync([NotNull] DocumentTypeDescriptionChanged baseEvent, DocumentTypeSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentTypeSummaryViewModel?>(new DocumentTypeSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
        }

        return Task.FromResult<DocumentTypeSummaryViewModel?>(summary with { Name = baseEvent.Name });
    }
}