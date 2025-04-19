// <copyright file="DocumentDescriptionChangedOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.Documents.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Requests.Documents;

/// <summary>
/// Handles the projection update when a document description is changed.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentDescriptionChangedOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentDescriptionChangedOnDetailsProjectionHandler(IProjectionFactory<DocumentDetailsViewModel> factory)
    : DocumentDetailsProjectionHandler<DocumentDescriptionChanged>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentDetailsViewModel?> ApplyEventAsync([NotNull] DocumentDescriptionChanged baseEvent, DocumentDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null)
        {
            return Task.FromResult<DocumentDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentDetailsViewModel?>(model with { Description = model.Description with { Name = baseEvent.Name, Comments = baseEvent.Comments } });
    }
}