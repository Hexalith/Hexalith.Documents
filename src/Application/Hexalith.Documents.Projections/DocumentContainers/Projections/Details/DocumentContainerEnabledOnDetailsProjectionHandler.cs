﻿namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handles the projection update when a document container is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentContainerEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerEnabledOnDetailsProjectionHandler(IProjectionFactory<DocumentContainerDetailsViewModel> factory)
    : DocumentContainerDetailsProjectionHandler<DocumentContainerEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerDetailsViewModel?> ApplyEventAsync([NotNull] DocumentContainerEnabled baseEvent, DocumentContainerDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled == false)
        {
            return Task.FromResult<DocumentContainerDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentContainerDetailsViewModel?>(model with { Disabled = false });
    }
}