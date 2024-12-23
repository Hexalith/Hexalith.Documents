namespace Hexalith.Documents.Projections.DocumentPartitions.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Events.DocumentPartitions;
using Hexalith.Documents.Projections.DocumentPartitions.Projections.Details;
using Hexalith.Documents.Projections.DocumentPartitions.Projections.Summaries;
using Hexalith.Documents.Projections.DocumentPartitions.RequestHandlers;
using Hexalith.Documents.Projections.DocumentPartitions.Services;
using Hexalith.Documents.Requests.DocumentPartitions;
using Hexalith.Documents.UI.Services.DocumentPartitions.Projections.Summaries;
using Hexalith.Documents.UI.Services.DocumentPartitions.Services;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding document partition request handlers to the service collection.
/// </summary>
public static class DocumentPartitionProjectionsHelper
{
    /// <summary>
    /// Adds the document partition projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentPartitionProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionAdded>, IdsCollectionProjectionHandler<DocumentPartitionAdded>>()

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionAdded>, DocumentPartitionAddedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionDescriptionChanged>, DocumentPartitionDescriptionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionDisabled>, DocumentPartitionDisabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionEnabled>, DocumentPartitionEnabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentPartitionSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionAdded>, DocumentPartitionAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionConnectionStringChanged>, DocumentPartitionConnectionStringChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionDescriptionChanged>, DocumentPartitionDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentPartitionDetailsSnapshotHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionDisabled>, DocumentPartitionDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentPartitionEnabled>, DocumentPartitionEnabledOnDetailsProjectionHandler>();

        return services;
    }

    /// <summary>
    /// Adds the document partition query services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentPartitionQueryServices(this IServiceCollection services)
    {
        services.TryAddScoped<IDocumentPartitionQueryService, DocumentPartitionQueryService>();
        return services;
    }

    /// <summary>
    /// Adds the document partition request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentPartitionRequestHandlers(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<GetDocumentPartition>, GetDocumentPartitionHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentPartitionDetails>, GetDocumentPartitionDetailsHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentPartitionSummaries>, GetDocumentPartitionSummariesHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentPartitionIds>, GetDocumentPartitionIdsHandler>();
        return services;
    }
}