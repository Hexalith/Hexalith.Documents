namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a DocumentTypeTagRemoved event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTypeTagRemovedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTypeTagRemovedOnDetailsProjectionHandler(IProjectionFactory<DocumentTypeDetailsViewModel> factory) : DocumentTypeDetailsProjectionHandler<DocumentTypeTagRemoved>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTypeTagRemoved baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentTypeDetailsViewModel?>(model with
        {
            Tags = [.. model.Tags.Where(p => p.Key != baseEvent.Key)],
        });
    }
}