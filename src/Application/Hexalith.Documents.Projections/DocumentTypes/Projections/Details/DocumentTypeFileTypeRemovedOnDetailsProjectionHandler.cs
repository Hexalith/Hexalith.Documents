namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a DocumentTypeFileTypeRemoved event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTypeFileTypeRemovedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTypeFileTypeRemovedOnDetailsProjectionHandler(IProjectionFactory<DocumentTypeDetailsViewModel> factory) : DocumentTypeDetailsProjectionHandler<DocumentTypeFileTypeRemoved>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTypeFileTypeRemoved baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentTypeDetailsViewModel?>(model with
        {
            FileTypeIds = model.FileTypeIds.Where(p => p != baseEvent.FileTypeId),
        });
    }
}