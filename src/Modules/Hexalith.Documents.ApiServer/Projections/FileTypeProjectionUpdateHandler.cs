namespace Hexalith.Documents.ApiServer.Projections;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Hexalith.Application.Metadatas;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Domain.Aggregates;
using Hexalith.Infrastructure.DaprRuntime.Projections;

using Microsoft.Extensions.Logging;

/// <summary>
/// Abstract base class for handling updates to FileType projections based on events.
/// </summary>
/// <typeparam name="TFileTypeEvent">The type of the file type event.</typeparam>
/// <param name="factory">The actor projection factory.</param>
/// <param name="logger">The logger instance.</param>
public abstract partial class FileTypeProjectionUpdateHandler<TFileTypeEvent>(IActorProjectionFactory<FileType> factory, ILogger logger)
    : KeyValueActorProjectionUpdateEventHandlerBase<TFileTypeEvent, FileType>(factory)
    where TFileTypeEvent : FileTypeEvent
{
    /// <inheritdoc/>
    public override async Task ApplyAsync([NotNull] TFileTypeEvent baseEvent, Metadata metadata, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(baseEvent);
        ArgumentNullException.ThrowIfNull(metadata);

        if (baseEvent is FileTypeAdded registered)
        {
            await SaveProjectionAsync(baseEvent.AggregateId, new FileType(registered), cancellationToken).ConfigureAwait(false);
            return;
        }

        FileType? existingFileType = await GetProjectionAsync(baseEvent.AggregateId, cancellationToken).ConfigureAwait(false);
        if (existingFileType == null)
        {
            FileTypeProjectionUpdateHandler<TFileTypeEvent>.LogFileTypeProjectionNotInitialized(
                logger,
                metadata.Message.Name,
                metadata.AggregateGlobalId,
                metadata.Message.Id,
                metadata.Context.CorrelationId);
            return;
        }

        ApplyResult result = existingFileType.Apply(baseEvent);
        if (result.Failed)
        {
            FileTypeProjectionUpdateHandler<TFileTypeEvent>.LogFileTypeProjectionCouldNotBeApplied(
                logger,
                metadata.Message.Name,
                metadata.AggregateGlobalId,
                metadata.Message.Id,
                metadata.Context.CorrelationId);
            return;
        }

        await SaveProjectionAsync(baseEvent.AggregateId, (FileType)result.Aggregate, cancellationToken).ConfigureAwait(false);
    }

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Error,
        Message = "The event {MessageName} is ignored. The event cannot be applied to the FileType projection for aggregate global Id '{AggregateGlobalId}'. MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogFileTypeProjectionCouldNotBeApplied(
        ILogger logger,
        string messageName,
        string aggregateGlobalId,
        string messageId,
        string correlationId);

    [LoggerMessage(
            EventId = 1,
        Level = LogLevel.Error,
        Message = "The event {MessageName} is ignored. The FileType projection not initialized for aggregate global Id '{AggregateGlobalId}'. MessageId='{MessageId}'; CorrelationId='{CorrelationId}'.")]
    private static partial void LogFileTypeProjectionNotInitialized(
        ILogger logger,
        string messageName,
        string aggregateGlobalId,
        string messageId,
        string correlationId);
}