namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handles the projection update when a document container is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentContainerDisabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerDisabledOnDetailsProjectionHandler(IProjectionFactory<DocumentContainerDetailsViewModel> factory)
    : DocumentContainerDetailsProjectionHandler<DocumentContainerDisabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerDetailsViewModel?> ApplyEventAsync([NotNull] DocumentContainerDisabled baseEvent, DocumentContainerDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled)
        {
            return Task.FromResult<DocumentContainerDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentContainerDetailsViewModel?>(model with { Disabled = true });
    }
}