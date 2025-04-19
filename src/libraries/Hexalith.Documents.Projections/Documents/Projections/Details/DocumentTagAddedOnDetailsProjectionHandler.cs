// <copyright file="DocumentTagAddedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.Documents.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;
using Hexalith.Documents.ValueObjects;

/// <summary>
/// Handles the projection update when a DocumentTargetAdded event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTargetAddedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTagAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentDetailsViewModel> factory) : DocumentDetailsProjectionHandler<DocumentTagAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTagAdded baseEvent, DocumentDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentDetailsViewModel?>(null);
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
        return Task.FromResult<DocumentDetailsViewModel?>(model with
        {
            Tags = [.. tags],
        });
    }
}