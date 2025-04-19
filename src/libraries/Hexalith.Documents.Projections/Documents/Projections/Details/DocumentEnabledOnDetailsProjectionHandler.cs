// <copyright file="DocumentEnabledOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.Documents.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handles the projection update when a document is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentEnabledOnDetailsProjectionHandler(IProjectionFactory<DocumentDetailsViewModel> factory)
    : DocumentDetailsProjectionHandler<DocumentEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentDetailsViewModel?> ApplyEventAsync([NotNull] DocumentEnabled baseEvent, DocumentDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || !model.Disabled)
        {
            return Task.FromResult<DocumentDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentDetailsViewModel?>(model with { Disabled = false });
    }
}