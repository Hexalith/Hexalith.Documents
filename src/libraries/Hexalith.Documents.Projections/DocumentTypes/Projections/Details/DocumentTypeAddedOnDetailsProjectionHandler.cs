// <copyright file="DocumentTypeAddedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentTypes.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Requests.DocumentTypes;

/// <summary>
/// Handles the projection update when a document type is added.
/// </summary>
/// <param name="factory">The factory.</param>
public class DocumentTypeAddedOnDetailsProjectionHandler(IProjectionFactory<DocumentTypeDetailsViewModel> factory)
    : DocumentTypeDetailsProjectionHandler<DocumentTypeAdded>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentTypeDetailsViewModel?> ApplyEventAsync([NotNull] DocumentTypeAdded baseEvent, DocumentTypeDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        return Task.FromResult<DocumentTypeDetailsViewModel?>(new DocumentTypeDetailsViewModel(
            baseEvent.Id,
            baseEvent.Name,
            baseEvent.Description,
            [],
            baseEvent.FileTypeIds,
            [],
            false));
    }
}