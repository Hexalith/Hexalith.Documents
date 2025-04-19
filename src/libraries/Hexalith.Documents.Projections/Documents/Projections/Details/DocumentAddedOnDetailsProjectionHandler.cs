// <copyright file="DocumentAddedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.Documents.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Documents;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;
using Hexalith.Documents.ValueObjects;

/// <summary>
/// Handles the projection update when a document is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentDetailsViewModel> factory)
    : DocumentDetailsProjectionHandler<DocumentAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentDetailsViewModel?> ApplyEventAsync([NotNull] DocumentAdded baseEvent, DocumentDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentDetailsViewModel?>(new DocumentDetailsViewModel(
            baseEvent.Id,
            new DocumentDescription(
                baseEvent.Name,
                baseEvent.Comments,
                baseEvent.DocumentContainerId,
                baseEvent.DocumentTypeId,
                null),
            new DocumentRouting(baseEvent.OwnerId, [], []),
            baseEvent.DocumentTypeId,
            DocumentState.Create(baseEvent.CreatedOn, baseEvent.OwnerId),
            [new DocumentActor(baseEvent.OwnerId, DocumentActorRole.Owner)],
            baseEvent.Files,
            [],
            false));
    }
}