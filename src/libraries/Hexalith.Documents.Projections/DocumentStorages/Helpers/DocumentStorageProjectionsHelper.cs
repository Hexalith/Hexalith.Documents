// <copyright file="DocumentStorageProjectionsHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentStorages.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.DocumentStorages;
using Hexalith.Documents.Events.DocumentStorages;
using Hexalith.Documents.Projections.DocumentStorages.Projections.Details;
using Hexalith.Documents.Projections.DocumentStorages.Projections.Summaries;
using Hexalith.Documents.Projections.DocumentStorages.RequestHandlers;
using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Documents.UI.Services.DocumentStorages.Projections.Summaries;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding document partition request handlers to the service collection.
/// </summary>
public static class DocumentStorageProjectionsHelper
{
    /// <summary>
    /// Adds the document partition projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentStorageProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<DocumentStorageAdded>, IdsCollectionProjectionHandler<DocumentStorageAdded>>()

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<DocumentStorageAdded>, DocumentStorageAddedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentStorageDescriptionChanged>, DocumentStorageDescriptionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentStorageDisabled>, DocumentStorageDisabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentStorageEnabled>, DocumentStorageEnabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentStorageSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<DocumentStorageAdded>, DocumentStorageAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentStorageTypeChanged>, DocumentStorageConnectionStringChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentStorageDescriptionChanged>, DocumentStorageDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentStorageDetailsSnapshotHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentStorageDisabled>, DocumentStorageDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentStorageEnabled>, DocumentStorageEnabledOnDetailsProjectionHandler>();

        return services;
    }

    /// <summary>
    /// Adds the document partition request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentStorageRequestHandlers(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<GetDocumentStorage>, GetDocumentStorageHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentStorageSummaries>, GetFilteredCollectionHandler<GetDocumentStorageSummaries, DocumentStorageSummaryViewModel>>();
        services.TryAddScoped<IRequestHandler<GetDocumentStorageDetails>, GetDocumentStorageDetailsHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentStorageExports>, GetExportsRequestHandler<GetDocumentStorageExports, DocumentStorage>>();
        services.TryAddScoped<IRequestHandler<GetDocumentStorageIds>, GetAggregateIdsRequestHandler<GetDocumentStorageIds>>();
        return services;
    }
}