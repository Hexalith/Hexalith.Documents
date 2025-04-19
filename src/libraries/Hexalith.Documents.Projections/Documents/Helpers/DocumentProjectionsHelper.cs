namespace Hexalith.Documents.Projections.Documents.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Documents;
using Hexalith.Documents.Events;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.Projections.Documents.Projections.Details;
using Hexalith.Documents.Projections.Documents.Projections.Summaries;
using Hexalith.Documents.Projections.Documents.RequestHandlers;
using Hexalith.Documents.Requests.Documents;
using Hexalith.Documents.UI.Services.Documents.Projections.Summaries;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding document request handlers to the service collection.
/// </summary>
public static class DocumentProjectionsHelper
{
    /// <summary>
    /// Adds the document projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<DocumentAdded>, IdsCollectionProjectionHandler<DocumentAdded>>()

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<DocumentAdded>, DocumentAddedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentDescriptionChanged>, DocumentDescriptionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentDisabled>, DocumentDisabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentEnabled>, DocumentEnabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<DocumentAdded>, DocumentAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentDescriptionChanged>, DocumentDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentDetailsSnapshotHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentDisabled>, DocumentDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentEnabled>, DocumentEnabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTagAdded>, DocumentTagAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTagRemoved>, DocumentTagRemovedOnDetailsProjectionHandler>();

        return services;
    }

    /// <summary>
    /// Adds the document request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentRequestHandlers(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<GetDocumentDetails>, GetDocumentDetailsHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentsInContainer>, GetDocumentsInContainerHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentSummaries>, GetFilteredCollectionHandler<GetDocumentSummaries, DocumentSummaryViewModel>>();
        services.TryAddScoped<IRequestHandler<GetDocumentExports>, GetExportsRequestHandler<GetDocumentExports, Document>>();
        services.TryAddScoped<IRequestHandler<GetDocumentIds>, GetAggregateIdsRequestHandler<GetDocumentIds>>();
        return services;
    }
}