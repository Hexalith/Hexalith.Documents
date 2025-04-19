// <copyright file="DocumentCommandHandlerHelper.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Application.Documents;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.Documents;
using Hexalith.Documents.Documents;
using Hexalith.Documents.Events.Documents;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Helper class for adding document command handlers to the service collection.
/// </summary>
public static class DocumentCommandHandlerHelper
{
    /// <summary>
    /// Adds the document command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentCommandHandlers(this IServiceCollection services) => services
            .TryAddSimpleInitializationCommandHandler<AddDocument>(
                c => new DocumentAdded(
                c.Id,
                c.Name,
                c.Comments,
                c.Files,
                c.OwnerId,
                c.CreatedOn,
                c.ParentDocumentId,
                c.DocumentContainerId,
                c.DocumentTypeId,
                c.Tags),
                ev => new Document((DocumentAdded)ev))
            .TryAddSimpleCommandHandler<EnableDocument>(c => new DocumentEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableDocument>(c => new DocumentDisabled(c.Id))
            .TryAddSimpleCommandHandler<AddDocumentAccessKey>(c => new DocumentAccessKeyAdded(c.Id, c.AccessKey))
            .TryAddSimpleCommandHandler<ChangeDocumentDescription>(c => new DocumentDescriptionChanged(
                c.Id,
                c.Name,
                c.Comments));
}