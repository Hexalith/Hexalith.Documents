namespace Hexalith.Documents.ApiServer.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.UI.Services.FileTypes.ViewModels;
using Hexalith.Documents.UI.Services.Helpers;
using Hexalith.Infrastructure.DaprRuntime.Helpers;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Class PartiesWebApiHelpers.
/// </summary>
public static class DocumentsWebApiHelpers
{
    /// <summary>
    /// Adds the customer projections.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="applicationId">Name of the application.</param>
    /// <returns>IServiceCollection.</returns>
    /// <exception cref="ArgumentNullException">null.</exception>
    public static IServiceCollection AddDocumentsProjections(this IServiceCollection services, string applicationId)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationId);
        _ = services.AddDocumentProjectionHandlers();
        _ = services.AddActorProjectionFactory<FileType>(applicationId);
        _ = services.AddActorProjectionFactory<FileTypeSummaryViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<FileTypeDetailsViewModel>(applicationId);
        _ = services.AddActorProjectionFactory<IdCollection>(applicationId);
        return services;
    }
}