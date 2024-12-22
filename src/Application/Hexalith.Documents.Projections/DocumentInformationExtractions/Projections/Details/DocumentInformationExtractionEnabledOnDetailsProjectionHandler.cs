namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Handles the projection update when a document information extraction is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentInformationExtractionEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentInformationExtractionEnabledOnDetailsProjectionHandler(IProjectionFactory<DocumentInformationExtractionDetailsViewModel> factory)
    : DocumentInformationExtractionDetailsProjectionHandler<DocumentInformationExtractionEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentInformationExtractionDetailsViewModel?> ApplyEventAsync([NotNull] DocumentInformationExtractionEnabled baseEvent, DocumentInformationExtractionDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled == false)
        {
            return Task.FromResult<DocumentInformationExtractionDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentInformationExtractionDetailsViewModel?>(model with { Disabled = false });
    }
}