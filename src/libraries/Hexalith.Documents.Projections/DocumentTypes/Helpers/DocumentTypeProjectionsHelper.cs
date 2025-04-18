namespace Hexalith.Documents.Projections.DocumentTypes.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Documents.Projections.DocumentTypes.Projections.Details;
using Hexalith.Documents.Projections.DocumentTypes.Projections.Summaries;
using Hexalith.Documents.Projections.DocumentTypes.RequestHandlers;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Documents.UI.Services.DocumentTypes.Projections.Summaries;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding document type request handlers to the service collection.
/// </summary>
public static class DocumentTypeProjectionsHelper
{
    /// <summary>
    /// Adds the document type projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentTypeProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<DocumentTypeAdded>, IdsCollectionProjectionHandler<DocumentTypeAdded>>()

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<DocumentTypeAdded>, DocumentTypeAddedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeDescriptionChanged>, DocumentTypeDescriptionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeDisabled>, DocumentTypeDisabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeEnabled>, DocumentTypeEnabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentTypeSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<DocumentTypeAdded>, DocumentTypeAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeDataExtractionAdded>, DocumentTypeDataExtractionAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeDataExtractionRemoved>, DocumentTypeDataExtractionRemovedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeFileTypeAdded>, DocumentTypeFileTypeAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeFileTypeRemoved>, DocumentTypeFileTypeRemovedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeDescriptionChanged>, DocumentTypeDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, DocumentTypeDetailsSnapshotHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeDisabled>, DocumentTypeDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeEnabled>, DocumentTypeEnabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeTagAdded>, DocumentTypeTagAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<DocumentTypeTagRemoved>, DocumentTypeTagRemovedOnDetailsProjectionHandler>();

        return services;
    }

    /// <summary>
    /// Adds the document type request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentTypeRequestHandlers(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<GetDocumentTypeDetails>, GetDocumentTypeDetailsHandler>();
        services.TryAddScoped<IRequestHandler<GetDocumentTypeSummaries>, GetFilteredCollectionHandler<GetDocumentTypeSummaries, DocumentTypeSummaryViewModel>>();
        services.TryAddScoped<IRequestHandler<GetDocumentTypeIds>, GetAggregateIdsRequestHandler<GetDocumentTypeIds>>();
        services.TryAddScoped<IRequestHandler<GetDocumentTypeExports>, GetExportsRequestHandler<GetDocumentTypeExports, DocumentType>>();
        return services;
    }
}