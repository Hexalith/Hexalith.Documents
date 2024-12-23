namespace Hexalith.Documents.Projections.DataExports.Helpers;

using Hexalith.Application.Requests;
using Hexalith.Documents.Projections.DataExports.RequestHandlers;
using Hexalith.Documents.Requests.DataExports;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding data export request handlers to the service collection.
/// </summary>
public static class DataExportProjectionsHelper
{
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