// <copyright file="DocumentInformationExtractionEnabledOnDetailsProjectionHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Details;

using System.Diagnostics.CodeAnalysis;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

/// <summary>
/// Handles the projection update when a document information extraction is enabled.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DocumentInformationExtractionEnabledOnDetailsProjectionHandler"/> class.
/// </remarks>
/// <param name="factory">The projection factory.</param>
public class DocumentInformationExtractionEnabledOnDetailsProjectionHandler(IProjectionFactory<DocumentInformationExtractionDetailsViewModel> factory)
    : DocumentInformationExtractionDetailsProjectionHandler<DocumentInformationExtractionEnabled>(factory)
{
    /// <inheritdoc/>
    protected override Task<DocumentInformationExtractionDetailsViewModel?> ApplyEventAsync([NotNull] DocumentInformationExtractionEnabled baseEvent, DocumentInformationExtractionDetailsViewModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        if (model == null || !model.Disabled)
        {
            return Task.FromResult<DocumentInformationExtractionDetailsViewModel?>(null);
        }

        return Task.FromResult<DocumentInformationExtractionDetailsViewModel?>(model with { Disabled = false });
    }
}