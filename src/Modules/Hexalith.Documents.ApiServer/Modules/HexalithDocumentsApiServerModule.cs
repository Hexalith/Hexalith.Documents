namespace Hexalith.Documents.ApiServer.Modules;

using System.Collections.Generic;

using Dapr.Actors.Runtime;

using Hexalith.Application.Modules.Modules;
using Hexalith.Application.Services;
using Hexalith.Documents.ApiServer.Controllers;
using Hexalith.Documents.ApiServer.Helpers;
using Hexalith.Documents.Application.Helpers;
using Hexalith.Documents.Application.Modules;
using Hexalith.Documents.Commands.Extensions;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Events.Extensions;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Documents.UI.Services;
using Hexalith.Documents.UI.Services.Helpers;
using Hexalith.Extensions.Configuration;
using Hexalith.Infrastructure.AzureBlobStorage.Configurations;
using Hexalith.Infrastructure.AzureBlobStorage.Services;
using Hexalith.Infrastructure.CosmosDb.Configurations;
using Hexalith.Infrastructure.DaprRuntime.Actors;
using Hexalith.Infrastructure.DaprRuntime.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// The document construction site client module.
/// </summary>
public sealed class HexalithDocumentsApiServerModule : IApiServerApplicationModule, IDocumentModule
{
    /// <inheritdoc/>
    public IDictionary<string, AuthorizationPolicy> AuthorizationPolicies => DocumentModulePolicies.AuthorizationPolicies;

    /// <inheritdoc/>
    public IEnumerable<string> Dependencies => [];

    /// <inheritdoc/>
    public string Description => "Hexalith Document API Server module";

    /// <inheritdoc/>
    public string Id => "Hexalith.Documents.ApiServer";

    /// <inheritdoc/>
    public string Name => "Hexalith Document API Server";

    /// <inheritdoc/>
    public int OrderWeight => 0;

    /// <inheritdoc/>
    string IApplicationModule.Path => Path;

    /// <inheritdoc/>
    public string Version => "1.0";

    private static string Path => nameof(Documents);

    /// <summary>
    /// Adds services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        _ = services
            .ConfigureSettings<CosmosDbSettings>(configuration)
            .ConfigureSettings<AzureBlobFileServiceSettings>(configuration);

        _ = services.AddScoped<IFileService, AzureBlobStorageFileService>();
        HexalithDocumentsEvents.RegisterPolymorphicMappers();
        HexalithDocumentsCommands.RegisterPolymorphicMappers();

        // Add application module
        services.TryAddSingleton<IDocumentModule, HexalithDocumentsApiServerModule>();

        // Add command handlers
        _ = services
            .AddDocumentManagement()
            .AddDocumentsProjections(nameof(Hexalith.Documents))
            .AddDocumentProjectionHandlers();

        _ = services
         .AddControllers()
         .AddApplicationPart(typeof(DocumentsIntegrationEventsController).Assembly);
    }

    /// <summary>
    /// Registers the actors associated with the module.
    /// </summary>
    /// <param name="actorCollection">The actor collection.</param>
    public static void RegisterActors(object actorCollection)
    {
        ArgumentNullException.ThrowIfNull(actorCollection);
        if (actorCollection is not ActorRegistrationCollection actorRegistrations)
        {
            throw new ArgumentException($"{nameof(RegisterActors)} parameter must be an {nameof(ActorRegistrationCollection)}. Actual type : {actorCollection.GetType().Name}.", nameof(actorCollection));
        }

        actorRegistrations.RegisterActor<DomainAggregateActor>(DomainAggregateActorBase.GetAggregateActorName(DocumentDomainHelper.FileTypeAggregateName));
        actorRegistrations.RegisterActor<DomainAggregateActor>(DomainAggregateActorBase.GetAggregateActorName(DocumentDomainHelper.DocumentAggregateName));
        actorRegistrations.RegisterProjectionActor<FileType>(nameof(Hexalith.Documents));
        actorRegistrations.RegisterProjectionActor<FileTypeSummaryViewModel>(nameof(Hexalith.Documents));
        actorRegistrations.RegisterProjectionActor<FileTypeDetailsViewModel>(nameof(Hexalith.Documents));
        actorRegistrations.RegisterProjectionActor<Document>(nameof(Hexalith.Documents));
        actorRegistrations.RegisterActor<SequentialStringListActor>(DocumentUIConstants.FileTypeIdsProjectionName);
    }

    /// <inheritdoc/>
    public void UseModule(object application)
    {
    }
}