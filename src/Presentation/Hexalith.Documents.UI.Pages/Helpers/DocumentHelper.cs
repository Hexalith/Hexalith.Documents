namespace Hexalith.Documents.UI.Pages.Helpers;

using Hexalith.Documents.UI.Components.Documents.Services;
using Hexalith.Documents.UI.Components.DocumentTypes.Services;
using Hexalith.Documents.UI.Pages.Documents.Services;
using Hexalith.Documents.UI.Pages.DocumentTypes.Services;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides helper methods for document-related services.
/// </summary>
public static class DocumentHelper
{
    /// <summary>
    /// Adds document UI services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The service collection with the added services.</returns>
    public static IServiceCollection AddDocumentUI(this IServiceCollection services)
    {
        _ = services.AddScoped<IDocumentQueryService, DemoDocumentQueryService>();
        _ = services.AddScoped<IDocumentTypeQueryService, DemoDocumentTypeQueryService>();
        return services;
    }
}