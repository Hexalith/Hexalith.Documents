namespace Hexalith.Documents.Application.DocumentStorages;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.DocumentStorages;
using Hexalith.Documents.Domain.DocumentStorages;
using Hexalith.Documents.Events.DocumentStorages;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Class DocumentHelper.
/// </summary>
public static class DocumentStorageCommandHandlerHelper
{
    /// <summary>
    /// Adds the file type command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentStorageCommandHandlers(this IServiceCollection services) => services
            .TryAddSimpleInitializationCommandHandler<AddDocumentStorage>(
                c => new DocumentStorageAdded(
                c.Id,
                c.Name,
                c.StorageType,
                c.Comments,
                c.ConnectionString),
                ev => new DocumentStorage((DocumentStorageAdded)ev))
            .TryAddSimpleCommandHandler<EnableDocumentStorage>(c => new DocumentStorageEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableDocumentStorage>(c => new DocumentStorageDisabled(c.Id))
            .TryAddSimpleCommandHandler<ChangeDocumentStorageDescription>(c => new DocumentStorageDescriptionChanged(
                c.Id,
                c.Name,
                c.Comments));
}