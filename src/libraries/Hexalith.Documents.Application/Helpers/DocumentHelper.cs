// <copyright file="DocumentHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Application.Helpers;

using FluentValidation;

using Hexalith.Application.Aggregates;
using Hexalith.Documents.Application.DataManagements;
using Hexalith.Documents.Application.DocumentContainers;
using Hexalith.Documents.Application.DocumentInformationExtractions;
using Hexalith.Documents.Application.Documents;
using Hexalith.Documents.Application.DocumentStorages;
using Hexalith.Documents.Application.DocumentTypes;
using Hexalith.Documents.Application.FileTypes;
using Hexalith.Documents.Application.Services;
using Hexalith.Documents.Commands.Documents;
using Hexalith.Documents.DataManagements;
using Hexalith.Documents.DocumentContainers;
using Hexalith.Documents.DocumentInformationExtractions;
using Hexalith.Documents.Documents;
using Hexalith.Documents.DocumentStorages;
using Hexalith.Documents.DocumentTypes;
using Hexalith.Documents.FileTypes;

using Microsoft.Extensions.DependencyInjection;

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
            .AddSingleton<IDomainAggregateProvider, DomainAggregateProvider<DocumentStorage>>()
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
    public static IServiceCollection AddDocumentEventValidators(this IServiceCollection services)
        => services.AddTransient<IValidator<AddDocument>, AddDocumentValidator>();

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
        => services
            .AddScoped<IUserDataService, UserDataService>()
            .AddDataManagementCommandHandlers()
            .AddDocumentContainerCommandHandlers()
            .AddDocumentInformationExtractionCommandHandlers()
            .AddDocumentStorageCommandHandlers()
            .AddDocumentCommandHandlers()
            .AddDocumentTypeCommandHandlers()
            .AddFileTypeCommandHandlers();
}