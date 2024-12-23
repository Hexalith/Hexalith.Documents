namespace Hexalith.Documents.Application.DocumentTypes;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.DocumentTypes;
using Hexalith.Documents.Domain.DocumentTypes;
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
                c.FileTypeIds),
                ev => new DocumentType((DocumentTypeAdded)ev))
            .TryAddSimpleCommandHandler<EnableDocumentType>(c => new DocumentTypeEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableDocumentType>(c => new DocumentTypeDisabled(c.Id))
            .TryAddSimpleCommandHandler<ChangeDocumentTypeDescription>(c => new DocumentTypeDescriptionChanged(
                c.Id,
                c.Name,
                c.Description));
}