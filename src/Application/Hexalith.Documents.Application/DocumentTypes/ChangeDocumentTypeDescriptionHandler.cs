namespace Hexalith.Documents.Application.DocumentTypes;

using System;

using Hexalith.Application.Commands;
using Hexalith.Application.Metadatas;
using Hexalith.Documents.Commands.DocumentTypes;
using Hexalith.Documents.Domain.DocumentTypes;
using Hexalith.Documents.Events.DocumentTypes;
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
public class ChangeDocumentTypeDescriptionHandler(
    TimeProvider timeProvider,
    ILogger<ChangeDocumentTypeDescriptionHandler> logger)
    : DomainCommandHandler<ChangeDocumentTypeDescription>(timeProvider, logger)
{
    /// <inheritdoc/>
    /// <remarks>
    /// Creates a new document or applies changes to an existing one by generating and applying
    /// a DocumentCreated event. If the aggregate is null, a new Document is created. If the
    /// aggregate exists, the event is applied to it.
    /// </remarks>
    public override Task<ExecuteCommandResult> DoAsync(ChangeDocumentTypeDescription command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        DocumentTypeDescriptionChanged ev = new(
            command.Id,
            command.Name,
            command.Description);

        return Task.FromResult(CheckAggregateIsValid<DocumentType>(aggregate, metadata)
            .Apply(ev)
            .CreateCommandResult(ev, metadata, Time));
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Rolls back a document creation by disabling the document through a DocumentDisabled event.
    /// This operation can only be performed on an existing document aggregate.
    /// </remarks>
    public override Task<ExecuteCommandResult> UndoAsync(ChangeDocumentTypeDescription command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
        => Task.FromException<ExecuteCommandResult>(new NotSupportedException());
}