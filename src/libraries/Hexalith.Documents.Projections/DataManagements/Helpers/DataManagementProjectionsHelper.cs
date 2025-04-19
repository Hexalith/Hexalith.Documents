// <copyright file="DataManagementProjectionsHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DataManagements.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.DataManagements;
using Hexalith.Documents.Events.DataManagements;
using Hexalith.Documents.Projections.DataManagements.Projections.Details;
using Hexalith.Documents.Projections.DataManagements.Projections.Summaries;
using Hexalith.Documents.Projections.DataManagements.RequestHandlers;
using Hexalith.Documents.Requests.DataManagements;
using Hexalith.Documents.UI.Services.DataManagements.Projections.Summaries;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding data export request handlers to the service collection.
/// </summary>
public static class DataManagementProjectionsHelper
{
    /// <summary>
    /// Adds the data export projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDataManagementProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<DataExportStarted>, IdsCollectionProjectionHandler<DataExportStarted>>()

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<DataExportStarted>, DataManagementStartedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DataExportCompleted>, DataManagementCompletedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DataManagementSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<DataManagementCommentsChanged>, DataManagementCommentsChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DataExportStarted>, DataManagementStartedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DataExportCompleted>, DataManagementCompletedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DataManagementDetailsSnapshotHandler>();

        return services;
    }

    /// <summary>
    /// Adds the data export request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDataManagementRequestHandlers(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<GetDataManagementDetails>, GetDataManagementDetailsHandler>();
        services.TryAddScoped<IRequestHandler<GetDataManagementSummaries>, GetFilteredCollectionHandler<GetDataManagementSummaries, DataManagementSummaryViewModel>>();
        services.TryAddScoped<IRequestHandler<GetDataManagementExports>, GetExportsRequestHandler<GetDataManagementExports, DataManagement>>();
        services.TryAddScoped<IRequestHandler<GetDataManagementIds>, GetAggregateIdsRequestHandler<GetDataManagementIds>>();
        return services;
    }
}