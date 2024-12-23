namespace Hexalith.Documents.Projections.Helpers;

using Hexalith.Documents.Projections.DataExports.Helpers;
using Hexalith.Documents.Projections.DataExports.Services;
using Hexalith.Documents.Projections.DocumentContainers.Helpers;
using Hexalith.Documents.Projections.DocumentContainers.Services;
using Hexalith.Documents.Projections.DocumentInformationExtractions.Helpers;
using Hexalith.Documents.Projections.DocumentInformationExtractions.Services;
using Hexalith.Documents.Projections.DocumentPartitions.Helpers;
using Hexalith.Documents.Projections.DocumentPartitions.Services;
using Hexalith.Documents.Projections.Documents.Helpers;
using Hexalith.Documents.Projections.Documents.Services;
using Hexalith.Documents.Projections.DocumentTypes.Helpers;
using Hexalith.Documents.Projections.DocumentTypes.Services;
using Hexalith.Documents.Projections.FileTypes.Helpers;
using Hexalith.Documents.Projections.FileTypes.Services;
using Hexalith.Documents.UI.Services.DataExports.Services;
using Hexalith.Documents.UI.Services.DocumentContainers.Services;
using Hexalith.Documents.UI.Services.DocumentInformationExtractions.Services;
using Hexalith.Documents.UI.Services.DocumentPartitions.Services;
using Hexalith.Documents.UI.Services.Documents.Services;
using Hexalith.Documents.UI.Services.DocumentTypes.Services;
using Hexalith.Documents.UI.Services.FileTypes.Services;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for adding document projections to the service collection.
/// </summary>
public static class DocumentProjectionHelper
{
    /// <summary>
    /// Adds document projections to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the projections to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentProjections(this IServiceCollection services)
        => services
            .AddDocumentsProjectionHandlers()
            .AddDocumentsQueryServices()
            .AddDocumentsRequestHandlers();

    /// <summary>
    /// Adds document projection handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentsProjectionHandlers(this IServiceCollection services)
        => services
            .AddFileTypeProjectionHandlers();

    /// <summary>
    /// Adds document UI services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The service collection with the added services.</returns>
    public static IServiceCollection AddDocumentsQueryServices(this IServiceCollection services)
    {
        _ = services.AddFileTypeQueryServices();
        _ = services.AddScoped<IDataExportQueryService, DataExportQueryService>();
        _ = services.AddScoped<IDocumentContainerQueryService, DocumentContainerQueryService>();
        _ = services.AddScoped<IDocumentInformationExtractionQueryService, DocumentInformationExtractionQueryService>();
        _ = services.AddScoped<IDocumentPartitionQueryService, DocumentPartitionQueryService>();
        _ = services.AddScoped<IDocumentQueryService, DocumentQueryService>();
        _ = services.AddScoped<IDocumentTypeQueryService, DocumentTypeQueryService>();
        _ = services.AddScoped<IFileTypeQueryService, FileTypeQueryService>();
        return services;
    }

    /// <summary>
    /// Adds document request handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentsRequestHandlers(this IServiceCollection services) => services
        .AddDataExportRequestHandlers()
        .AddDocumentContainerRequestHandlers()
        .AddDocumentInformationExtractionRequestHandlers()
        .AddDocumentPartitionRequestHandlers()
        .AddDocumentRequestHandlers()
        .AddDocumentTypeRequestHandlers()
        .AddFileTypeRequestHandlers();
}