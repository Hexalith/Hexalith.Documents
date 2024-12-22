namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

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
public class DocumentTypeAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentTypeDetailsViewModel> factory)
    : DocumentTypeDetailsProjectionHandler<DocumentTypeAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTypeAdded baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentTypeDetailsViewModel?>(new DocumentTypeDetailsViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.Description,
            [],
            baseEvent.FileTypeIds,
            [],
            false));
    }
}