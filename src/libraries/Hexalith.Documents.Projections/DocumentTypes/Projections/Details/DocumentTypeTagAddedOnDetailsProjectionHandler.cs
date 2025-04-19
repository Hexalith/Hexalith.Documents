// <copyright file="DocumentTypeTagAddedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Documents.ValueObjects;

/// <summary>
/// Handles the projection update when a DocumentTypeTagAdded event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentTypeTagAddedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentTypeTagAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentTypeDetailsViewModel> factory) : DocumentTypeDetailsProjectionHandler<DocumentTypeTagAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTypeTagAdded baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentTypeDetailsViewModel?>(null);
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
        return Task.FromResult<DocumentTypeDetailsViewModel?>(model with
        {
            Tags = [.. tags],
        });
    }
}