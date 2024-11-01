namespace Hexalith.Documents.Application.CommandHandlers;

using Hexalith.Application.Commands;
using Hexalith.Application.Metadatas;
using Hexalith.Documents.Commands;
using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Events;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Command handler for adding a new factory.
/// </summary>
public class CreateDocumentHandler : DomainCommandHandler<CreateDocument>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDocumentHandler"/> class.
    /// </summary>
    /// <param name="timeProvider"></param>
    public CreateDocumentHandler(TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);
        TimeProvider = timeProvider;
    }

    public TimeProvider TimeProvider { get; }

    /// <inheritdoc/>
    public override Task<ExecuteCommandResult> DoAsync(CreateDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        DocumentCreated ev = new(
            command.Id,
            command.Name,
            command.Description,
            command.LocationUrl,
            command.OwnerId,
            TimeProvider.GetLocalNow(),
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
    public override Task<ExecuteCommandResult> UndoAsync(CreateDocument command, Metadata metadata, IDomainAggregate? aggregate, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        return Task.FromException<ExecuteCommandResult>(new NotSupportedException("Undo operation is not supported for adding a document."));
    }
}