namespace Hexalith.Documents.ApiServer.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Documents.ApiServer.Controllers;
using Hexalith.Documents.ApiServer.Projections;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Domain.Events;
using Hexalith.Infrastructure.DaprRuntime.Helpers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        services.TryAddScoped<IProjectionUpdateHandler<SnapshotEvent>, FileTypeSnapshotHandler>();
        services.TryAddScoped<IProjectionUpdateHandler<FileTypeAdded>, FileTypeAddedProjectionHandler>();
        _ = services.AddActorProjectionFactory<FileType>(applicationId);
        _ = services
         .AddControllers()
         .AddApplicationPart(typeof(DocumentsIntegrationEventsController).Assembly);
        return services;
    }
}