namespace Hexalith.Documents.Application.Documents;

using Hexalith.Application.Commands;
using Hexalith.Application.Metadatas;
using Hexalith.Documents.Commands.Documents;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Events;
using Hexalith.Documents.Events.Documents;
using Hexalith.Domain.Aggregates;

using Microsoft.Extensions.Logging;

/// <summary>
/// Handles the creation of documents by processing CreateDocument commands.
/// </summary>
/// <remarks>
/// This handler is responsible for creating new documents and managing their lifecycle
/// through domain events. It implements the command handling pattern for document creation
/// and provides both execution and rollback capabilities.
/// </remarks>
public class AddDocumentHandler(TimeProvider timeProvider, ILogger<AddDocumentHandler> logger)
    : DomainCommandHandler<AddDocument>(timeProvider, logger)
{
    /// <inheritdoc/>
    /// <remarks>
    /// Creates a new document or applies changes to an existing one by generating and applying
    /// a DocumentCreated event. If the aggregate is null, a new Document is created. If the
    /// aggregate exists, the event is applied to it.
    /// </remarks>
    public override Task<ExecuteCommandResult> DoAsync(AddDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        DocumentAdded ev = new(
            command.Id,
            command.Name,
            command.Description,
            command.File,
            command.OwnerId,
            command.CreatedOn,
            command.DocumentTypeId);

        if (aggregate is null)
        {
            return Task.FromResult(new ExecuteCommandResult(new Document(ev), [ev], [ev], false));
        }

        return Task.FromResult(CheckAggregateIsValid<Document>(aggregate, metadata)
            .Apply(ev)
            .CreateCommandResult(ev, metadata, Time));
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Rolls back a document creation by disabling the document through a DocumentDisabled event.
    /// This operation can only be performed on an existing document aggregate.
    /// </remarks>
    public override Task<ExecuteCommandResult> UndoAsync(AddDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        DocumentDisabled ev = new(command.Id);
        return Task.FromResult(CheckAggregateIsValid<Document>(aggregate, metadata)
            .Apply(ev)
            .CreateCommandResult(ev, metadata, Time));
    }
}