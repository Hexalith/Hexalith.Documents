namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a DocumentTypeDataExtractionRemoved event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTypeDataExtractionRemovedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTypeDataExtractionRemovedOnDetailsProjectionHandler(IProjectionFactory<DocumentTypeDetailsViewModel> factory) : DocumentTypeDetailsProjectionHandler<DocumentTypeDataExtractionRemoved>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTypeDataExtractionRemoved baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentTypeDetailsViewModel?>(model with
        {
            DataExtractionIds = model.DataExtractionIds.Where(p => p != baseEvent.DataInformationExtractionId),
        });
    }
}