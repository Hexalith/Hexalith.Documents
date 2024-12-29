namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

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
/// Initializes a new instance of the <see cref="DocumentTypeDescriptionChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTypeDescriptionChangedOnDetailsProjectionHandler(IProjectionFactory<DocumentTypeDetailsViewModel> factory)
    : DocumentTypeDetailsProjectionHandler<DocumentTypeDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTypeDescriptionChanged baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentTypeDetailsViewModel?>(model with { Name = baseEvent.Name, Comments = baseEvent.Description });
    }
}