namespace Hexalith.Documents.Application.Helpers;

using Hexalith.Application.Aggregates;
using Hexalith.Application.Commands;
using Hexalith.Documents.Application.DataManagements;
using Hexalith.Documents.Application.DocumentContainers;
using Hexalith.Documents.Application.DocumentInformationExtractions;
using Hexalith.Documents.Application.DocumentPartitions;
using Hexalith.Documents.Application.Documents;
using Hexalith.Documents.Application.DocumentTypes;
using Hexalith.Documents.Application.FileTypes;
using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Commands.DataManagements;
using Hexalith.Documents.Domain.DataManagements;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Domain.DocumentInformationExtractions;
using Hexalith.Documents.Domain.DocumentPartitions;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Domain.FileTypes;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Class DocumentHelper.
/// </summary>
public static class DocumentHelper
{
    /// <summary>
    /// Adds the document aggregate providers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentAggregateProviders(this IServiceCollection services)
    {
        _ = services
            .AddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DataManagement>>()
            .AddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DocumentContainer>>()
            .AddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DocumentInformationExtraction>>()
            .AddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DocumentPartition>>()
            .AddSingleton<IDomainAggregateProvider, DomainAggregateProvider<Document>>()
            .AddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DocumentType>>()
            .AddSingleton<IDomainAggregateProvider, DomainAggregateProvider<FileType>>();
        return services;
    }

    /// <summary>
    /// Adds the document event validators to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentEventValidators(this IServiceCollection services) =>

        // services.TryAddSingleton<IValidator<>, >();
        services;

    /// <summary>
    /// Adds the document management services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentManagement(this IServiceCollection services)
    {
        _ = services.AddDocumentsCommandHandlers();
        _ = services.AddDocumentAggregateProviders();
        _ = services.AddDocumentEventValidators();
        return services;
    }

    /// <summary>
    /// Adds the document command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentsCommandHandlers(this IServiceCollection services)
    {
        _ = services.AddScoped<IUserDataService, UserDataService>();

        // Needs to be Transient for the file stream to be disposed
        services.TryAddTransient<IDomainCommandHandler<ExportRequestDataToDocument>, ExportRequestDataToDocumentHandler>();
        _ = services.AddDocumentContainerCommandHandlers();
        _ = services.AddDocumentInformationExtractionCommandHandlers();
        _ = services.AddDocumentPartitionCommandHandlers();
        _ = services.AddDocumentCommandHandlers();
        _ = services.AddDocumentTypeCommandHandlers();
        _ = services.AddFileTypeCommandHandlers();
        return services;
    }
}