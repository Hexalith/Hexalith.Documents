namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handles the projection update when a document container description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentContainerDescriptionChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerDescriptionChangedOnDetailsProjectionHandler(IProjectionFactory<DocumentContainerDetailsViewModel> factory)
    : DocumentContainerDetailsProjectionHandler<DocumentContainerDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerDetailsViewModel?> ApplyEventAsync([NotNull] DocumentContainerDescriptionChanged baseEvent, DocumentContainerDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentContainerDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentContainerDetailsViewModel?>(model with { Name = baseEvent.Name, Comments = baseEvent.Comments });
    }
}