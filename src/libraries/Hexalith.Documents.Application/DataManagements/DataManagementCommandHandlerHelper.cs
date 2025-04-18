namespace Hexalith.Documents.Application.DataManagements;

using Hexalith.Application.Commands;
using Hexalith.Documents.Commands.DataManagements;
using Hexalith.Documents.Events.DataManagements;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides helper methods for adding file type command handlers to the service collection.
/// </summary>
public static class DataManagementCommandHandlerHelper
{
    /// <summary>
    /// Adds the file type command handlers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDataManagementCommandHandlers(this IServiceCollection services)
        => services

            // Needs to be Transient for the file stream to be disposed
            .AddTransient<IDomainCommandHandler<ExportRequestDataToDocument>, ExportRequestDataToDocumentHandler>()
            .TryAddSimpleCommandHandler<ChangeDataManagementComments>(c => new DataManagementCommentsChanged(
                c.Id,
                c.Comments));
}