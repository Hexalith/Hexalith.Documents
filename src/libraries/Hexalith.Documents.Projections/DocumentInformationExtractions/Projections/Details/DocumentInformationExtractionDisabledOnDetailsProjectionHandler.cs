// <copyright file="DocumentInformationExtractionDisabledOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Details;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Handles the projection update when a document information extraction is disabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentInformationExtractionDisabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentInformationExtractionDisabledOnDetailsProjectionHandler(IProjectionFactory<DocumentInformationExtractionDetailsViewModel> factory)
    : DocumentInformationExtractionDetailsProjectionHandler<DocumentInformationExtractionDisabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentInformationExtractionDetailsViewModel?> ApplyEventAsync([NotNull] DocumentInformationExtractionDisabled baseEvent, DocumentInformationExtractionDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || model.Disabled)
        {
            return Task.FromResult<DocumentInformationExtractionDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentInformationExtractionDetailsViewModel?>(model with { Disabled = true });
    }
}