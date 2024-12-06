namespace Hexalith.Documents.ApiServer.Projections;

using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Infrastructure.DaprRuntime.Projections;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the projection update when a file type is added.
/// </summary>
/// <param name="factory">The factory.</param>
/// <param name="logger">The logger.</param>
public class FileTypeAddedProjectionHandler(
    IActorProjectionFactory<FileType> factory,
    ILogger<FileTypeAddedProjectionHandler> logger)
    : FileTypeProjectionUpdateHandler<FileTypeAdded>(factory, logger)
{
}