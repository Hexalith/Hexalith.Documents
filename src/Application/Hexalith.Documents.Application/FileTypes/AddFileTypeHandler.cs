namespace Hexalith.Documents.Application.FileTypes;

using Hexalith.Application.Commands;
using Hexalith.Application.Metadatas;
using Hexalith.Documents.Commands.FileTypes;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.Documents.Events;
using Hexalith.Documents.Events.FileTypes;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Handles the creation of documents by processing CreateDocument commands.
/// </summary>
/// <remarks>
/// This handler is responsible for creating new documents and managing their lifecycle
/// through domain events. It implements the command handling pattern for document creation
/// and provides both execution and rollback capabilities.
/// </remarks>
public class AddFileTypeHandler : DomainCommandHandler<AddFileType>
{
    private readonly TimeProvider _timeProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddFileTypeHandler"/> class.
    /// </summary>
    /// <param name="timeProvider">The time provider to get the current time.</param>
    public AddFileTypeHandler(TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);
        _timeProvider = timeProvider;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Creates a new document or applies changes to an existing one by generating and applying
    /// a DocumentCreated event. If the aggregate is null, a new Document is created. If the
    /// aggregate exists, the event is applied to it.
    /// </remarks>
    public override Task<ExecuteCommandResult> DoAsync(
        AddFileType command,
        Metadata metadata,
        IDomainAggregate? aggregate,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        FileTypeAdded ev = new(
            command.Id,
            command.Name,
            command.Description,
            command.FileToTextConverter,
            command.Targets);

        if (aggregate is null)
        {
            return Task.FromResult(new ExecuteCommandResult(new FileType(ev), [ev], [ev]));
        }

        if (aggregate is FileType a)
        {
            ApplyResult result = a.Apply(ev);
            return Task.FromResult(result.CreateCommandResult(
                ev,
                metadata,
                _timeProvider));
        }

        return Task.FromException<ExecuteCommandResult>(new InvalidAggregateTypeException<Document>(aggregate));
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Rolls back a document creation by disabling the document through a DocumentDisabled event.
    /// This operation can only be performed on an existing document aggregate.
    /// </remarks>
    public override Task<ExecuteCommandResult> UndoAsync(AddFileType command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        DocumentDisabled ev = new(command.Id);
        if (aggregate is null)
        {
            return Task.FromException<ExecuteCommandResult>(new NotSupportedException("Cannot undo a command that has not been executed. Aggregate is null."));
        }

        if (aggregate is Document factory)
        {
            ApplyResult result = factory.Apply(ev);
            return Task.FromResult(new ExecuteCommandResult(aggregate, result.Failed ? [] : [ev], result.Messages));
        }

        return Task.FromException<ExecuteCommandResult>(new InvalidAggregateTypeException<Document>(aggregate));
    }
}