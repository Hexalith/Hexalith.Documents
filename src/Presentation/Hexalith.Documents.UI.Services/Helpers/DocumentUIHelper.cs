﻿namespace Hexalith.Documents.UI.Services.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Documents.UI.Pages.Documents.Services;
using Hexalith.Documents.UI.Pages.DocumentTypes.Services;
using Hexalith.Documents.UI.Services.Documents.Services;
using Hexalith.Documents.UI.Services.DocumentTypes.Services;
using Hexalith.Documents.UI.Services.FileTypes.Projections.Details;
using Hexalith.Documents.UI.Services.FileTypes.Projections.Summaries;
using Hexalith.Documents.UI.Services.FileTypes.RequestHandlers;
using Hexalith.Documents.UI.Services.FileTypes.Services;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Provides extension methods for adding document projections to the service collection.
/// </summary>
public static class DocumentUIHelper
{
    /// <summary>
    /// Adds document projection handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentProjectionHandlers(this IServiceCollection services)
    {
        services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<FileTypeAdded>, IdsCollectionProjectionHandler<FileTypeAdded>>()
            .TryAddScoped<IProjectionUpdateHandler<SnapshotEvent>, IdsCollectionProjectionHandler<SnapshotEvent>>();

        _ = services

            // Summary projections
            .AddScoped<IProjectionUpdateHandler<FileTypeAdded>, FileTypeAddedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDescriptionChanged>, FileTypeDescriptionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDisabled>, FileTypeDisabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeEnabled>, FileTypeEnabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, FileTypeSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<FileTypeAdded>, FileTypeAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDescriptionChanged>, FileTypeDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDisabled>, FileTypeDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeEnabled>, FileTypeEnabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeFileToTextConverterChanged>, FileTypeFileToTextConverterChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeTargetAdded>, FileTypeTargetAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeTargetRemoved>, FileTypeTargetRemovedOnDetailsProjectionHandler>();

        return services;
    }

    /// <summary>
    /// Adds document request handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentRequestHandlers(this IServiceCollection services) => services
        .AddScoped<IRequestHandler<GetFileTypeSummaries>, GetFileTypeSummariesHandler>()
        .AddScoped<IRequestHandler<GetFileTypeDetails>, GetFileTypeDetailsHandler>();

    /// <summary>
    /// Adds document UI services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The service collection with the added services.</returns>
    public static IServiceCollection AddDocumentUIServices(this IServiceCollection services)
    {
        _ = services.AddScoped<IDocumentQueryService, DemoDocumentQueryService>();
        _ = services.AddScoped<IDocumentTypeQueryService, DemoDocumentTypeQueryService>();
        _ = services.AddScoped<IFileTypeQueryService, FileTypeQueryService>();
        _ = services.AddScoped<IDocumentUploadService, DocumentUploadService>();
        return services;
    }
}