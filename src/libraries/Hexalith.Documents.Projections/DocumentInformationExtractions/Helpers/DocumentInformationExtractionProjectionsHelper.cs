namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.DocumentInformationExtractions;
using Hexalith.Documents.Events.DocumentInformationExtractions;
using Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Details;
using Hexalith.Documents.Projections.DocumentInformationExtractions.Projections.Summaries;
using Hexalith.Documents.Projections.DocumentInformationExtractions.RequestHandlers;
using Hexalith.Documents.Requests.DocumentInformationExtractions;
using Hexalith.Documents.UI.Services.DocumentInformationExtractions.Projections.Summaries;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding document information extraction request handlers to the service collection.
/// </summary>
public static class DocumentInformationExtractionProjectionsHelper
{
    /// <summary>
    /// Adds the document information extraction projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentInformationExtractionProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionAdded>, IdsCollectionProjectionHandler<DocumentInformationExtractionAdded>>()

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionAdded>, DocumentInformationExtractionAddedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionDescriptionChanged>, DocumentInformationExtractionDescriptionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionDisabled>, DocumentInformationExtractionDisabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionEnabled>, DocumentInformationExtractionEnabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentInformationExtractionSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionAdded>, DocumentInformationExtractionAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionDescriptionChanged>, DocumentInformationExtractionDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentInformationExtractionDetailsSnapshotHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionDisabled>, DocumentInformationExtractionDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentInformationExtractionEnabled>, DocumentInformationExtractionEnabledOnDetailsProjectionHandler>();

        return services;
    }

    /// <summary>
    /// Adds the document information extraction request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentInformationExtractionRequestHandlers(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<GetDocumentInformationExtractionDetails>, GetDocumentInformationExtractionDetailsHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentInformationExtractionSummaries>, GetFilteredCollectionHandler<GetDocumentInformationExtractionSummaries, DocumentInformationExtractionSummaryViewModel>>();
        services.TryAddScoped<IRequestHandler<GetDocumentInformationExtractionExports>, GetExportsRequestHandler<GetDocumentInformationExtractionExports, DocumentInformationExtraction>>();
        services.TryAddScoped<IRequestHandler<GetDocumentInformationExtractionIds>, GetAggregateIdsRequestHandler<GetDocumentInformationExtractionIds>>();
        return services;
    }
}