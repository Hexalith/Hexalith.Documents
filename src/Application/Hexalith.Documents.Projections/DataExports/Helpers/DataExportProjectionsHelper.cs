namespace Hexalith.Documents.Projections.DataExports.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Events.DataExports;
using Hexalith.Documents.Projections.DataExports.Projections.Details;
using Hexalith.Documents.Projections.DataExports.Projections.Summaries;
using Hexalith.Documents.Projections.DataExports.RequestHandlers;
using Hexalith.Documents.Projections.DataExports.Services;
using Hexalith.Documents.Requests.DataExports;
using Hexalith.Documents.UI.Services.DataExports.Projections.Summaries;
using Hexalith.Documents.UI.Services.DataExports.Services;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding data export request handlers to the service collection.
/// </summary>
public static class DataExportProjectionsHelper
{
    /// <summary>
    /// Adds the data export projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDataExportProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<DataExportStarted>, IdsCollectionProjectionHandler<DataExportStarted>>()

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<DataExportStarted>, DataExportStartedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DataExportCompleted>, DataExportCompletedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DataExportSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<DataExportStarted>, DataExportStartedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DataExportCompleted>, DataExportCompletedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DataExportDetailsSnapshotHandler>();

        return services;
    }

    /// <summary>
    /// Adds the data export query services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDataExportQueryServices(this IServiceCollection services)
    {
        services.TryAddScoped<IDataExportQueryService, DataExportQueryService>();
        return services;
    }

    /// <summary>
    /// Adds the data export request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDataExportRequestHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<IRequestHandler<GetDataExportDetails>, GetDataExportDetailsHandler>();
        services.TryAddSingleton<IRequestHandler<GetDataExportSummaries>, GetDataExportSummariesHandler>();
        services.TryAddSingleton<IRequestHandler<GetDataExportIds>, GetDataExportIdsHandler>();
        return services;
    }
}