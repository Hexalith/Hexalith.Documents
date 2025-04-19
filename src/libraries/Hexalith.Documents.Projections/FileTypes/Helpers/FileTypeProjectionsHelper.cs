// <copyright file="FileTypeProjectionsHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.FileTypes.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Application.Requests;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.FileTypes;
using Hexalith.Documents.Projections.FileTypes.Projections.Details;
using Hexalith.Documents.Projections.FileTypes.Projections.Summaries;
using Hexalith.Documents.Projections.FileTypes.RequestHandlers;
using Hexalith.Documents.Requests.FileTypes;
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
            .AddScoped<IProjectionUpdateHandler<FileTypeDescriptionChanged>, FileTypeDescriptionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeFileExtensionChanged>, FileTypeFileExtensionChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeContentTypeChanged>, FileTypeContentTypeChangedOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDisabled>, FileTypeDisabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeEnabled>, FileTypeEnabledOnSummaryProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<SnapshotEvent>, FileTypeSnapshotOnSummaryProjectionHandler>()

            // Details
            .AddScoped<IProjectionUpdateHandler<FileTypeAdded>, FileTypeAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDescriptionChanged>, FileTypeDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeContentTypeChanged>, FileTypeContentTypeChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeFileExtensionChanged>, FileTypeFileExtensionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDisabled>, FileTypeDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeEnabled>, FileTypeEnabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeFileToTextConverterChanged>, FileTypeFileToTextConverterChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeOtherContentTypeAdded>, FileTypeOtherContentTypeAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeOtherContentTypeRemoved>, FileTypeOtherContentTypeRemovedOnDetailsProjectionHandler>();

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
        services.TryAddScoped<IRequestHandler<GetFileTypeSummaries>, GetFilteredCollectionHandler<GetFileTypeSummaries, FileTypeSummaryViewModel>>();
        services.TryAddScoped<IRequestHandler<GetFileTypeExports>, GetExportsRequestHandler<GetFileTypeExports, FileType>>();
        services.TryAddScoped<IRequestHandler<GetFileTypeIds>, GetAggregateIdsRequestHandler<GetFileTypeIds>>();
        return services;
    }
}