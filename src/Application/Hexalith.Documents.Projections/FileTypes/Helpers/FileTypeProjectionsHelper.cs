﻿namespace Hexalith.Documents.Projections.FileTypes.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.Projections.FileTypes.Projections.Details;
using Hexalith.Documents.Projections.FileTypes.Projections.Summaries;
using Hexalith.Documents.Projections.FileTypes.RequestHandlers;
using Hexalith.Documents.Projections.FileTypes.Services;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Documents.UI.Services.FileTypes.Services;
using Hexalith.Domain.Events;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// Helper class for adding file type request handlers to the service collection.
/// </summary>
public static class FileTypeProjectionsHelper
{
    /// <summary>
    /// Adds the file type projection handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddFileTypeProjectionHandlers(this IServiceCollection services)
    {
        _ = services

            // Collection projections
            .AddScoped<IProjectionUpdateHandler<FileTypeAdded>, IdsCollectionProjectionHandler<FileTypeAdded>>()

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
    /// Adds the file type query services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddFileTypeQueryServices(this IServiceCollection services)
    {
        services.TryAddScoped<IFileTypeQueryService, FileTypeQueryService>();
        return services;
    }

    /// <summary>
    /// Adds the file type request handlers to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddFileTypeRequestHandlers(this IServiceCollection services)
    {
        services.TryAddScoped<IRequestHandler<GetFileTypeDetails>, GetFileTypeDetailsHandler>();
        services.TryAddScoped<IRequestHandler<GetFileTypeSummaries>, GetFileTypeSummariesHandler>();
        services.TryAddScoped<IRequestHandler<GetFileTypeIds>, GetFileTypeIdsHandler>();
        return services;
    }
}