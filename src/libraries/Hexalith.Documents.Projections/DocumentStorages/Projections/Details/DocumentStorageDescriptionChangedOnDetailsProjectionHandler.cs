namespace Hexalith.Documents.Projections.DocumentStorages.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handles the projection update when a document partition description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentStorageDescriptionChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentStorageDescriptionChangedOnDetailsProjectionHandler(IProjectionFactory<DocumentStorageDetailsViewModel> factory)
    : DocumentStorageDetailsProjectionHandler<DocumentStorageDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentStorageDetailsViewModel?> ApplyEventAsync([NotNull] DocumentStorageDescriptionChanged baseEvent, DocumentStorageDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentStorageDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentStorageDetailsViewModel?>(model with { Name = baseEvent.Name, Comments = baseEvent.Comments });
    }
}