// <copyright file="DocumentProjectionHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.Helpers;

using Hexalith.Documents.Projections.DataManagements.Helpers;
using Hexalith.Documents.Projections.DocumentContainers.Helpers;
using Hexalith.Documents.Projections.DocumentInformationExtractions.Helpers;
using Hexalith.Documents.Projections.Documents.Helpers;
using Hexalith.Documents.Projections.DocumentStorages.Helpers;
using Hexalith.Documents.Projections.DocumentTypes.Helpers;
using Hexalith.Documents.Projections.FileTypes.Helpers;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for adding document projections to the service collection.
/// </summary>
public static class DocumentProjectionHelper
{
    /// <summary>
    /// Adds document projections to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the projections to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentProjections(this IServiceCollection services)
        => services
            .AddDocumentsProjectionHandlers()
            .AddDocumentsQueryServices()
            .AddDocumentsRequestHandlers();

    /// <summary>
    /// Adds document projection handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentsProjectionHandlers(this IServiceCollection services)
        => services
            .AddDataManagementProjectionHandlers()
            .AddDocumentInformationExtractionProjectionHandlers()
            .AddDocumentContainerProjectionHandlers()
            .AddDocumentStorageProjectionHandlers()
            .AddDocumentProjectionHandlers()
            .AddDocumentTypeProjectionHandlers()
            .AddFileTypeProjectionHandlers();

    /// <summary>
    /// Adds document UI services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The service collection with the added services.</returns>
    public static IServiceCollection AddDocumentsQueryServices(this IServiceCollection services)
        => services;

    /// <summary>
    /// Adds document request handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentsRequestHandlers(this IServiceCollection services) => services
        .AddDataManagementRequestHandlers()
        .AddDocumentContainerRequestHandlers()
        .AddDocumentInformationExtractionRequestHandlers()
        .AddDocumentStorageRequestHandlers()
        .AddDocumentRequestHandlers()
        .AddDocumentTypeRequestHandlers()
        .AddFileTypeRequestHandlers();
}