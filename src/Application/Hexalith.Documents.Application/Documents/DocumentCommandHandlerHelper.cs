﻿namespace Hexalith.Documents.Application.Documents;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.Documents;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Events;
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
                c.Description,
                c.File,
                c.OwnerId,
                c.CreatedOn,
                c.DocumentTypeId),
                ev => new Document((DocumentAdded)ev))
            .TryAddSimpleCommandHandler<EnableDocument>(c => new DocumentEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableDocument>(c => new DocumentDisabled(c.Id))
            .TryAddSimpleCommandHandler<ChangeDocumentDescription>(c => new DocumentDescriptionChanged(
                c.Id,
                c.Name,
                c.Description));
}