﻿namespace Hexalith.Documents.Application.CommandHandlers;

using Hexalith.Application.Commands;
using Hexalith.Application.Metadatas;
using Hexalith.Documents.Commands;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Events;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Handles the creation of documents by processing CreateDocument commands.
/// </summary>
/// <remarks>
/// This handler is responsible for creating new documents and managing their lifecycle
/// through domain events. It implements the command handling pattern for document creation
/// and provides both execution and rollback capabilities.
/// </remarks>
public class CreateDocumentHandler : DomainCommandHandler<CreateDocument>
{
    /// <inheritdoc/>
    /// <remarks>
    /// Creates a new document or applies changes to an existing one by generating and applying
    /// a DocumentCreated event. If the aggregate is null, a new Document is created. If the
    /// aggregate exists, the event is applied to it.
    /// </remarks>
    public override Task<ExecuteCommandResult> DoAsync(CreateDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        DocumentCreated ev = new(
            command.Id,
            command.Name,
            command.Description,
            command.File,
            command.OwnerId,
            command.CreatedOn,
            command.DocumentTypeId);

        if (aggregate is null)
        {
            return Task.FromResult(new ExecuteCommandResult(new Document(ev), [ev], [ev]));
        }

        if (aggregate is Document factory)
        {
            ApplyResult result = factory.Apply(ev);
            return Task.FromResult(new ExecuteCommandResult(aggregate, result.Failed ? [] : [ev], result.Messages));
        }

        return Task.FromException<ExecuteCommandResult>(new InvalidAggregateTypeException<Document>(aggregate));
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Rolls back a document creation by disabling the document through a DocumentDisabled event.
    /// This operation can only be performed on an existing document aggregate.
    /// </remarks>
    public override Task<ExecuteCommandResult> UndoAsync(CreateDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
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
