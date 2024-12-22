namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a document type is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTypeEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTypeEnabledOnDetailsProjectionHandler(IProjectionFactory<DocumentTypeDetailsViewModel> factory)
    : DocumentTypeDetailsProjectionHandler<DocumentTypeEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTypeEnabled baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled == false)
        {
            return Task.FromResult<DocumentTypeDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentTypeDetailsViewModel?>(model with { Disabled = false });
    }
}