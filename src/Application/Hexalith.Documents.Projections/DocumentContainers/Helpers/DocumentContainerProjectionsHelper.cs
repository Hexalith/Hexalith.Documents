namespace Hexalith.Documents.Projections.DocumentContainers.Helpers;

using Hexalith.Application.Events;
using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Projections.DocumentContainers.Projections.Details;
using Hexalith.Documents.Projections.DocumentContainers.Projections.Documents;
using Hexalith.Documents.Projections.DocumentContainers.Projections.Summaries;
using Hexalith.Documents.Projections.DocumentContainers.RequestHandlers;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Documents.UI.Services.DocumentContainers.Projections.Summaries;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding document container request handlers to the service collection.
/// </summary>
public static class DocumentContainerProjectionsHelper
{
    /// <summary>
    /// Adds the document container projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentContainerProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<DocumentContainerCreated>, IdsCollectionProjectionHandler<DocumentContainerCreated>>()

            // Documents projections
            .AddScoped<IIntegrationEventHandler<DocumentAdded>, DocumentContainerDocumentAddedHandler>()

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<DocumentContainerCreated>, DocumentContainerCreatedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerDescriptionChanged>, DocumentContainerDescriptionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerDisabled>, DocumentContainerDisabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerEnabled>, DocumentContainerEnabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentContainerSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<DocumentContainerCreated>, DocumentContainerCreatedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerAutomaticRoutingInstructionsChanged>, DocumentContainerAutomaticRoutingInstructionsChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerDescriptionChanged>, DocumentContainerDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentContainerDetailsSnapshotHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerTagAdded>, DocumentContainerTagAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerTagRemoved>, DocumentContainerTagRemovedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerDisabled>, DocumentContainerDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentContainerEnabled>, DocumentContainerEnabledOnDetailsProjectionHandler>();

        return services;
    }

    /// <summary>
    /// Adds the document container request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentContainerRequestHandlers(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<GetDocumentContainerDetails>, GetDocumentContainerDetailsHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentContainerSummaries>, GetFilteredCollectionHandler<GetDocumentContainerSummaries, DocumentContainerSummaryViewModel>>();
        services.TryAddScoped<IRequestHandler<GetDocumentContainerExports>, GetExportsRequestHandler<GetDocumentContainerExports, DocumentContainer>>();
        services.TryAddScoped<IRequestHandler<GetDocumentContainerIds>, GetAggregateIdsRequestHandler<GetDocumentContainerIds>>();
        return services;
    }
}