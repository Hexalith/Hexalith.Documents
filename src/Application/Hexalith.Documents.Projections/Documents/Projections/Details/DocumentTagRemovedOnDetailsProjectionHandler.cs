namespace Hexalith.Documents.Projections.Documents.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handles the projection update when a DocumentTagRemoved event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTagRemovedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTagRemovedOnDetailsProjectionHandler(IProjectionFactory<DocumentDetailsViewModel> factory) : DocumentDetailsProjectionHandler<DocumentTagRemoved>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTagRemoved baseEvent, DocumentDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentDetailsViewModel?>(null);
        }

        return Task.FromResult(model with
        {
            Tags = [.. model.Tags.Where(p => p.Key != baseEvent.Key)],
        });
    }
}