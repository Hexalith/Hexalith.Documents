namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Summaries;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Handles the projection update when a document information extraction description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentInformationExtractionDescriptionChangedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentInformationExtractionDescriptionChangedOnSummaryProjectionHandler(IProjectionFactory<DocumentInformationExtractionSummaryViewModel> factory)
    : DocumentInformationExtractionSummaryProjectionHandler<DocumentInformationExtractionDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentInformationExtractionSummaryViewModel?> ApplyEventAsync([NotNull] DocumentInformationExtractionDescriptionChanged baseEvent, DocumentInformationExtractionSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentInformationExtractionSummaryViewModel?>(new DocumentInformationExtractionSummaryViewModel(baseEvent.Id, baseEvent.Name, false));
        }

        return Task.FromResult<DocumentInformationExtractionSummaryViewModel?>(summary with { Name = baseEvent.Name });
    }
}