// <copyright file="DocumentContainerTagRemovedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handles the projection update when a DocumentContainerTagRemoved event is received.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentContainerTagRemovedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerTagRemovedOnDetailsProjectionHandler(IProjectionFactory<DocumentContainerDetailsViewModel> factory) : DocumentContainerDetailsProjectionHandler<DocumentContainerTagRemoved>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerDetailsViewModel?> ApplyEventAsync([NotNull] DocumentContainerTagRemoved baseEvent, DocumentContainerDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentContainerDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentContainerDetailsViewModel?>(model with
        {
            Tags = [.. model.Tags.Where(p => p.Key != baseEvent.Key)],
        });
    }
}