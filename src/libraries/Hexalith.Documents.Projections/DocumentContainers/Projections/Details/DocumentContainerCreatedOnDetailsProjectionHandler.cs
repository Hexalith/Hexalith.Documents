// <copyright file="DocumentContainerCreatedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentContainers.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;

/// <summary>
/// Handles the projection update when a document container is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentContainerCreatedOnDetailsProjectionHandler(IProjectionFactory<DocumentContainerDetailsViewModel> factory)
    : DocumentContainerDetailsProjectionHandler<DocumentContainerCreated>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerDetailsViewModel?> ApplyEventAsync([NotNull] DocumentContainerCreated baseEvent, DocumentContainerDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentContainerDetailsViewModel?>(new DocumentContainerDetailsViewModel(
            baseEvent.Id,
            baseEvent.DocumentStorageId,
            baseEvent.Name,
            baseEvent.Path,
            baseEvent.Comments,
            null,
            [],
            [],
            [],
            false));
    }
}