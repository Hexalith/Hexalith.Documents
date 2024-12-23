namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Helpers;

using Hexalith.Application.Requests;
using Hexalith.Documents.Projections.DocumentInformationExtractions.RequestHandlers;
using Hexalith.Documents.Requests.DocumentInformationExtractions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding document information extraction request handlers to the service collection.
/// </summary>
public static class DocumentInformationExtractionProjectionsHelper
{
    /// <summary>
    /// Adds the document information extraction request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentInformationExtractionRequestHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<IRequestHandler<GetDocumentInformationExtractionDetails>, GetDocumentInformationExtractionDetailsHandler>();
        services.TryAddSingleton<IRequestHandler<GetDocumentInformationExtractionSummaries>, GetDocumentInformationExtractionSummariesHandler>();
        services.TryAddSingleton<IRequestHandler<GetDocumentInformationExtractionIds>, GetDocumentInformationExtractionIdsHandler>();
        return services;
    }
}