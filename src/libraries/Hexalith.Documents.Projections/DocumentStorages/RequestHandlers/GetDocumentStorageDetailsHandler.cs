// <copyright file="GetDocumentStorageDetailsHandler.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentStorages.RequestHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Requests.DocumentStorages;

/// <summary>
/// Handler for getting document partition details.
/// </summary>
public class GetDocumentStorageDetailsHandler : RequestHandlerBase<GetDocumentStorageDetails>
{
    private readonly IProjectionFactory<DocumentStorageDetailsViewModel> _projectionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageDetailsHandler"/> class.
    /// </summary>
    /// <param name="projectionFactory">The projection factory.</param>
    /// <exception cref="ArgumentNullException">Thrown when projectionFactory is null.</exception>
    public GetDocumentStorageDetailsHandler(IProjectionFactory<DocumentStorageDetailsViewModel> projectionFactory)
    {
        ArgumentNullException.ThrowIfNull(projectionFactory);
        _projectionFactory = projectionFactory;
    }

    /// <inheritdoc/>
    public override async Task<GetDocumentStorageDetails> ExecuteAsync(GetDocumentStorageDetails request, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(metadata);

        return request with
        {
            Result = await _projectionFactory
                .GetStateAsync(metadata.AggregateGlobalId, cancellationToken)
                .ConfigureAwait(false)
                    ?? throw new InvalidOperationException($"File type {metadata.AggregateGlobalId} not found."),
        };
    }
}