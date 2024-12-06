namespace Hexalith.Documents.UI.Services.Helpers;

using Hexalith.Application.Projections;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.UI.Services.FileTypes.Projections.Details;

using Microsoft.Extensions.DependencyInjection;

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
    public static IServiceCollection AddDocumentProjections(this IServiceCollection services)
    {
        _ = services
            .AddScoped<IProjectionUpdateHandler<FileTypeAdded>, FileTypeAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDescriptionChanged>, FileTypeDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDisabled>, FileTypeDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeEnabled>, FileTypeEnabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeAdded>, FileTypeAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDescriptionChanged>, FileTypeDescriptionChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeDisabled>, FileTypeDisabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeEnabled>, FileTypeEnabledOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeFileToTextConverterChanged>, FileTypeFileToTextConverterChangedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeTargetAdded>, FileTypeTargetAddedOnDetailsProjectionHandler>()
            .AddScoped<IProjectionUpdateHandler<FileTypeTargetRemoved>, FileTypeTargetRemovedOnDetailsProjectionHandler>();

        return services;
    }
}