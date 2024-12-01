namespace Hexalith.Documents.Application.Helpers;

using Hexalith.Application.Aggregates;
using Hexalith.Application.Commands;
using Hexalith.Documents.Application.Documents;
using Hexalith.Documents.Application.DocumentTypes;
using Hexalith.Documents.Application.FileTypes;
using Hexalith.Documents.Commands.Documents;
using Hexalith.Documents.Commands.DocumentTypes;
using Hexalith.Documents.Commands.FileTypes;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Domain.FileTypes;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Hexalith.Documents.Domain.FileTextExtractionModes;

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
        services.TryAddSingleton<IDomainAggregateProvider, DomainAggregateProvider<Document>>();
        services.TryAddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DocumentType>>();
        services.TryAddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DocumentContainer>>();
        services.TryAddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DocumentInformationExtraction>>();
        services.TryAddSingleton<IDomainAggregateProvider, DomainAggregateProvider<FileType>>();
        return services;
    }

    /// <summary>
    /// Adds the document command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentCommandHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<IDomainCommandHandler<AddDocument>, AddDocumentHandler>();
        services.TryAddSingleton<IDomainCommandHandler<AddDocumentType>, AddDocumentTypeHandler>();
        services.TryAddSingleton<IDomainCommandHandler<AddFileType>, AddFileTypeHandler>();
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
        _ = services.AddDocumentCommandHandlers();
        _ = services.AddDocumentAggregateProviders();
        _ = services.AddDocumentEventValidators();
        return services;
    }
}