// <copyright file="DocumentContainerDisabledOnDetailsProjectionHandler.cs" company="ITANEO">
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
/// Handles the projection update when a document container is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentContainerDisabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentContainerDisabledOnDetailsProjectionHandler(IProjectionFactory<DocumentContainerDetailsViewModel> factory)
    : DocumentContainerDetailsProjectionHandler<DocumentContainerDisabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentContainerDetailsViewModel?> ApplyEventAsync([NotNull] DocumentContainerDisabled baseEvent, DocumentContainerDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model?.Disabled != false)
        {
            return Task.FromResult<DocumentContainerDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentContainerDetailsViewModel?>(model with { Disabled = true });
    }
}