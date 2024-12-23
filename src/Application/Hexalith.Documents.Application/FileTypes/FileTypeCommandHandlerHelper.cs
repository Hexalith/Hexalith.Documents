namespace Hexalith.Documents.Application.FileTypes;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.FileTypes;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Events.FileTypes;

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
                c.Description,
                c.FileToTextConverter,
                c.Targets),
                ev => new FileType((FileTypeAdded)ev))
            .TryAddSimpleCommandHandler<AddFileTypeTarget>(c => new FileTypeTargetAdded(
                c.Id,
                c.Target))
            .TryAddSimpleCommandHandler<RemoveFileTypeTarget>(c => new FileTypeTargetRemoved(
                c.Id,
                c.Target))
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