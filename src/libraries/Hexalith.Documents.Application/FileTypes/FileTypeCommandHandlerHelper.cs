// <copyright file="FileTypeCommandHandlerHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Application.FileTypes;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.FileTypes;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Documents.FileTypes;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides helper methods for adding file type command handlers to the service collection.
/// </summary>
public static class FileTypeCommandHandlerHelper
{
    /// <summary>
    /// Adds the file type command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddFileTypeCommandHandlers(this IServiceCollection services) => services
            .TryAddSimpleInitializationCommandHandler<AddFileType>(
                c => new FileTypeAdded(
                c.Id,
                c.Name,
                c.ContentType,
                c.OtherContentTypes,
                c.FileExtension,
                c.Comments,
                c.FileToTextConverter),
                ev => new FileType((FileTypeAdded)ev))
            .TryAddSimpleCommandHandler<ChangeFileTypeContentType>(c => new FileTypeContentTypeChanged(
                c.Id,
                c.ContentType))
            .TryAddSimpleCommandHandler<AddFileTypeOtherContentType>(c => new FileTypeOtherContentTypeAdded(
                c.Id,
                c.OtherContentType))
            .TryAddSimpleCommandHandler<RemoveFileTypeOtherContentType>(c => new FileTypeOtherContentTypeRemoved(
                c.Id,
                c.OtherContentType))
            .TryAddSimpleCommandHandler<ChangeFileTypeFileExtension>(c => new FileTypeFileExtensionChanged(
                c.Id,
                c.FileExtension))
            .TryAddSimpleCommandHandler<EnableFileType>(c => new FileTypeEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableFileType>(c => new FileTypeDisabled(c.Id))
            .TryAddSimpleCommandHandler<ChangeFileTypeDescription>(c => new FileTypeDescriptionChanged(
                c.Id,
                c.Name,
                c.Description))
            .TryAddSimpleCommandHandler<ChangeFileTypeFileToTextConverter>(c => new FileTypeFileToTextConverterChanged(
                c.Id,
                c.FileToTextConverter));
}