namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Handles the projection update when a document information extraction is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentInformationExtractionAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentInformationExtractionDetailsViewModel> factory)
    : DocumentInformationExtractionDetailsProjectionHandler<DocumentInformationExtractionAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentInformationExtractionDetailsViewModel?> ApplyEventAsync([NotNull] DocumentInformationExtractionAdded baseEvent, DocumentInformationExtractionDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentInformationExtractionDetailsViewModel?>(
            new DocumentInformationExtractionDetailsViewModel(
                baseEvent.AggregateId,
                baseEvent.Name,
                baseEvent.Model,
                baseEvent.SystemMessage,
                baseEvent.OutputFormat,
                baseEvent.OutputSample,
                baseEvent.Instructions,
                baseEvent.ValidationModel,
                baseEvent.ValidationInstructions,
                baseEvent.Comments,
                false));
    }
}