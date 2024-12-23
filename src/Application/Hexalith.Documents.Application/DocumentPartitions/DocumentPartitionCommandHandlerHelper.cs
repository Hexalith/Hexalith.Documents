namespace Hexalith.Documents.Application.DocumentPartitions;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.DocumentPartitions;
using Hexalith.Documents.Domain.DocumentPartitions;
using Hexalith.Documents.Events.DocumentPartitions;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Class DocumentHelper.
/// </summary>
public static class DocumentPartitionCommandHandlerHelper
{
    /// <summary>
    /// Adds the file type command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentPartitionCommandHandlers(this IServiceCollection services) => services
            .TryAddSimpleInitializationCommandHandler<AddDocumentPartition>(
                c => new DocumentPartitionAdded(
                c.Id,
                c.Name,
                c.StorageType,
                c.Description,
                c.ConnectionString),
                ev => new DocumentPartition((DocumentPartitionAdded)ev))
            .TryAddSimpleCommandHandler<EnableDocumentPartition>(c => new DocumentPartitionEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableDocumentPartition>(c => new DocumentPartitionDisabled(c.Id))
            .TryAddSimpleCommandHandler<ChangeDocumentPartitionDescription>(c => new DocumentPartitionDescriptionChanged(
                c.Id,
                c.Name,
                c.Description));
}