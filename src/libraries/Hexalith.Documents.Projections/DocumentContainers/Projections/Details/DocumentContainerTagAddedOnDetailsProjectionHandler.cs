// <copyright file="DocumentContainerTagAddedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Documents.ValueObjects;

/// <summary>
/// Handles the projection update when a DocumentContainerTagAdded event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentContainerTagAddedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerTagAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentContainerDetailsViewModel> factory) : DocumentContainerDetailsProjectionHandler<DocumentContainerTagAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerDetailsViewModel?> ApplyEventAsync([NotNull] DocumentContainerTagAdded baseEvent, DocumentContainerDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentContainerDetailsViewModel?>(null);
        }

        IQueryable<DocumentTag> tags = model.Tags.AsQueryable();
        if (baseEvent.Unique)
        {
            tags = tags.Where(p => p.Key != baseEvent.Key);
        }

        tags = tags
            .Append(new DocumentTag(baseEvent.Key, baseEvent.Value, baseEvent.Unique))
            .Distinct()
            .OrderBy(p => p.Key)
            .ThenBy(p => p.Value);
        return Task.FromResult<DocumentContainerDetailsViewModel?>(model with
        {
            Tags = [.. tags],
        });
    }
}