namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handles the projection update when a document partition is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentStorageEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentStorageEnabledOnDetailsProjectionHandler(IProjectionFactory<DocumentStorageDetailsViewModel> factory)
    : DocumentStorageDetailsProjectionHandler<DocumentStorageEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentStorageDetailsViewModel?> ApplyEventAsync([NotNull] DocumentStorageEnabled baseEvent, DocumentStorageDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled == false)
        {
            return Task.FromResult<DocumentStorageDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentStorageDetailsViewModel?>(model with { Disabled = false });
    }
}