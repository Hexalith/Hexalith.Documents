namespace Hexalith.Documents.Shared.Modules;

using System.Collections.Generic;
using System.Reflection;


using Hexalith.Application.Aggregates;
using Hexalith.Application.Commands;
using Hexalith.Application.Modules.Modules;
using Hexalith.Documents.Application.CommandHandlers;
using Hexalith.Documents.Commands;
using Hexalith.Documents.Commands.Extensions;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Events.Extensions;
using Hexalith.Documents.Shared.Documents.Services;
using Hexalith.UI.Components;
using Hexalith.UI.Components.Icons;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// The document construction site shared module.
/// </summary>
public class DocumentSharedModule : ISharedApplicationModule
{
    /// <inheritdoc/>
    public IEnumerable<string> Dependencies => ["Hexalith.Oidc"];

    /// <inheritdoc/>
    public string Description => "Document shared module";

    /// <inheritdoc/>
    public string Id => "Document.Shared";

    /// <inheritdoc/>
    public string Name => "Document shared";

    /// <inheritdoc/>
    public int OrderWeight => 0;

    /// <inheritdoc/>
    public string Path => "Document";

    /// <inheritdoc/>
    public IEnumerable<Assembly> PresentationAssemblies => [GetType().Assembly];

    /// <inheritdoc/>
    public string Version => "1.0";

    /// <summary>
    /// Adds services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        HexalithDocumentsEvents.RegisterPolymorphicMappers();
        HexalithDocumentsCommands.RegisterPolymorphicMappers();

        // Add domain aggregate providers
        services.TryAddSingleton<IDomainAggregateProvider, DomainAggregateProvider<Document>>();

        // Add command handlers
        services.TryAddSingleton<IDomainCommandHandler<AddDocument>, AddDocumentHandler>();

        _ = services
            .AddSingleton<IDocumentQueryService, DemoDocumentQueryService>()
            .AddSingleton<IDocumentQueryService, DemoDocumentQueryService>()
            .AddSingleton(new MenuItemInformation(
                "Home",
                "/",
                new IconInformation("Home", 20, IconStyle.Regular, IconSource.Fluent, $"{nameof(Hexalith.Document)}.{nameof(Shared)}"),
                true,
                0,
                []))
            .AddTransient(p => DocumentMenu.Menu);
    }

    /// <inheritdoc/>
    public void UseModule(object builder)
    {
    }
}