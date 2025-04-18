namespace Hexalith.Documents.Application.DocumentContainers;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.DocumentContainers;
using Hexalith.Documents.Domain.DocumentContainers;
using Hexalith.Documents.Events.DocumentContainers;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides helper methods for adding document container command handlers to the service collection.
/// </summary>
public static class DocumentContainerCommandHandlerHelper
{
    /// <summary>
    /// Adds the document container command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentContainerCommandHandlers(this IServiceCollection services) => services
            .TryAddSimpleInitializationCommandHandler<CreateDocumentContainer>(
                c => new DocumentContainerCreated(
                c.Id,
                c.DocumentStorageId,
                c.Name,
                c.Path,
                c.Comments,
                c.AutomaticRoutingInstructions),
                ev => new DocumentContainer((DocumentContainerCreated)ev))
            .TryAddSimpleCommandHandler<EnableDocumentContainer>(c => new DocumentContainerEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableDocumentContainer>(c => new DocumentContainerDisabled(c.Id))
            .TryAddSimpleCommandHandler<ChangeDocumentContainerDescription>(c => new DocumentContainerDescriptionChanged(
                c.Id,
                c.Name,
                c.Comments));
}