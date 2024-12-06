namespace Hexalith.Documents.UI.Pages.Helpers;

using Hexalith.Documents.UI.Pages.Documents.Services;
using Hexalith.Documents.UI.Pages.DocumentTypes.Services;
using Hexalith.Documents.UI.Pages.FileTypes.Services;
using Hexalith.Documents.UI.Services.Documents.Services;
using Hexalith.Documents.UI.Services.DocumentTypes.Services;
using Hexalith.Documents.UI.Services.FileTypes.Services;

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
        _ = services.AddScoped<IFileTypeQueryService, DemoFileTypeQueryService>();
        _ = services.AddScoped<IDocumentUploadService, DocumentUploadService>();
        return services;
    }
}