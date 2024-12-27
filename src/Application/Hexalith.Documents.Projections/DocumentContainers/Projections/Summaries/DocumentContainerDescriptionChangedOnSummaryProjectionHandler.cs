namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Summaries;

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
/// Initializes a new instance of the <see cref="DocumentContainerDescriptionChangedOnSummaryProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerDescriptionChangedOnSummaryProjectionHandler(IProjectionFactory<DocumentContainerSummaryViewModel> factory)
    : DocumentContainerSummaryProjectionHandler<DocumentContainerDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerSummaryViewModel?> ApplyEventAsync([NotNull] DocumentContainerDescriptionChanged baseEvent, DocumentContainerSummaryViewModel? summary, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (summary == null)
        {
            return Task.FromResult<DocumentContainerSummaryViewModel?>(null);
        }

        return Task.FromResult<DocumentContainerSummaryViewModel?>(summary with { Name = baseEvent.Name });
    }
}