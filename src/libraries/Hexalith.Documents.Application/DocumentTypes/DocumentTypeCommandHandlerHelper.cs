// <copyright file="DocumentTypeCommandHandlerHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Application.DocumentTypes;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.DocumentTypes;
using Hexalith.Documents.DocumentTypes;
using Hexalith.Documents.Events.DocumentTypes;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Helper class for adding document type command handlers to the service collection.
/// </summary>
public static class DocumentTypeCommandHandlerHelper
{
    /// <summary>
    /// Adds the document type command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentTypeCommandHandlers(this IServiceCollection services) => services
            .TryAddSimpleInitializationCommandHandler<AddDocumentType>(
                c => new DocumentTypeAdded(
                c.Id,
                c.Name,
                c.Description,
                c.DataExtractionIds,
                c.FileTypeIds),
                ev => new DocumentType((DocumentTypeAdded)ev))
            .TryAddSimpleCommandHandler<EnableDocumentType>(c => new DocumentTypeEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableDocumentType>(c => new DocumentTypeDisabled(c.Id))
            .TryAddSimpleCommandHandler<AddDocumentTypeFileType>(c => new DocumentTypeFileTypeAdded(c.Id, c.FileTypeId))
            .TryAddSimpleCommandHandler<AddDocumentTypeTag>(c => new DocumentTypeTagAdded(c.Id, c.Key, c.Value, c.Unique))
            .TryAddSimpleCommandHandler<RemoveDocumentTypeTag>(c => new DocumentTypeTagRemoved(c.Id, c.Key, c.Value))
            .TryAddSimpleCommandHandler<RemoveDocumentTypeFileType>(c => new DocumentTypeFileTypeRemoved(c.Id, c.FileTypeId))
            .TryAddSimpleCommandHandler<AddDocumentTypeDataExtraction>(c => new DocumentTypeDataExtractionAdded(c.Id, c.DataInformationExtractionId))
            .TryAddSimpleCommandHandler<RemoveDocumentTypeDataExtraction>(c => new DocumentTypeDataExtractionRemoved(c.Id, c.DataInformationExtractionId))
            .TryAddSimpleCommandHandler<ChangeDocumentTypeDescription>(c => new DocumentTypeDescriptionChanged(
                c.Id,
                c.Name,
                c.Description));
}