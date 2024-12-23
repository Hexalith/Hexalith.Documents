namespace Hexalith.Documents.Projections.DocumentContainers.Helpers;

using Hexalith.Application.Requests;
using Hexalith.Documents.Projections.DocumentContainers.RequestHandlers;
using Hexalith.Documents.Requests.DocumentContainers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding document container request handlers to the service collection.
/// </summary>
public static class DocumentContainerProjectionsHelper
{
    /// <summary>
    /// Adds the document container request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentContainerRequestHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<IRequestHandler<GetDocumentContainerDetails>, GetDocumentContainerDetailsHandler>();
        services.TryAddSingleton<IRequestHandler<GetDocumentContainerSummaries>, GetDocumentContainerSummariesHandler>();
        services.TryAddSingleton<IRequestHandler<GetDocumentContainerIds>, GetDocumentContainerIdsHandler>();
        return services;
    }
}