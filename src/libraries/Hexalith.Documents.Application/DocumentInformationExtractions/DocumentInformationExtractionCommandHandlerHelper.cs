namespace Hexalith.Documents.Application.DocumentInformationExtractions;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.DocumentInformationExtractions;
using Hexalith.Documents.Domain.DocumentInformationExtractions;
using Hexalith.Documents.Events.DocumentInformationExtractions;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides helper methods for adding document information extraction command handlers to the service collection.
/// </summary>
public static class DocumentInformationExtractionCommandHandlerHelper
{
    /// <summary>
    /// Adds the document information extraction command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDocumentInformationExtractionCommandHandlers(this IServiceCollection services) => services
            .TryAddSimpleInitializationCommandHandler<AddDocumentInformationExtraction>(
                c => new DocumentInformationExtractionAdded(
                c.Id,
                c.Name,
                c.Model,
                c.SystemMessage,
                c.OutputFormat,
                c.OutputSample,
                c.Instructions,
                c.ValidationModel,
                c.ValidationInstructions,
                c.Comments),
                ev => new DocumentInformationExtraction((DocumentInformationExtractionAdded)ev))
            .TryAddSimpleCommandHandler<EnableDocumentInformationExtraction>(c => new DocumentInformationExtractionEnabled(c.Id))
            .TryAddSimpleCommandHandler<DisableDocumentInformationExtraction>(c => new DocumentInformationExtractionDisabled(c.Id))
            .TryAddSimpleCommandHandler<ChangeDocumentInformationExtractionDescription>(c => new DocumentInformationExtractionDescriptionChanged(
                c.Id,
                c.Name,
                c.Comments));
}