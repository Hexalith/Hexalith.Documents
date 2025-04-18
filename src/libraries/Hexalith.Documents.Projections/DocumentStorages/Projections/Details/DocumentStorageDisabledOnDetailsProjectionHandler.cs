namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handles the projection update when a document partition is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentStorageDisabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentStorageDisabledOnDetailsProjectionHandler(IProjectionFactory<DocumentStorageDetailsViewModel> factory)
    : DocumentStorageDetailsProjectionHandler<DocumentStorageDisabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentStorageDetailsViewModel?> ApplyEventAsync([NotNull] DocumentStorageDisabled baseEvent, DocumentStorageDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled)
        {
            return Task.FromResult<DocumentStorageDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentStorageDetailsViewModel?>(model with { Disabled = true });
    }
}