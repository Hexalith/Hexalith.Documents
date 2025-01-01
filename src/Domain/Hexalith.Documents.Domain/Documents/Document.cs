namespace Hexalith.Documents.Domain.Documents;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Events;
using Hexalith.Documents.Events.Documents;
using Hexalith.Domain.Aggregates;
using Hexalith.Domain.Events;

/// <summary>
/// Represents a document in the domain.
/// </summary>
/// <param name="Id">The ID of the document.</param>
/// <param name="Description">The description of the document.</param>
/// <param name="Routing">The routing of the document.</param>
/// <param name="ParentDocumentId">The ID of the parent document.</param>
/// <param name="State">The state of the document.</param>
/// <param name="Actors">The actors of the document.</param>
/// <param name="File">The file of the document.</param>
/// <param name="Tags">The tags of the document.</param>
/// <param name="Disabled">Indicates whether the document is disabled.</param>
[DataContract]
public record Document(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] DocumentDescription Description,
    [property: DataMember(Order = 3)] DocumentRouting? Routing,
    [property: DataMember(Order = 4)] string? ParentDocumentId,
    [property: DataMember(Order = 5)] DocumentState State,
    [property: DataMember(Order = 6)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 7)] FileDescription? File,
    [property: DataMember(Order = 8)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 9)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class.
    /// </summary>
    public Document()
        : this(
              string.Empty,
              new DocumentDescription(
                  string.Empty,
                  null,
                  null,
                  null,
                  null),
              null,
              null,
              DocumentState.Create(DateTimeOffset.MinValue, string.Empty),
              [],
              new FileDescription(string.Empty, string.Empty, string.Empty, 0L, string.Empty),
              [],
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class based on the <see cref="DocumentAdded"/> event.
    /// </summary>
    /// <param name="added">The <see cref="DocumentAdded"/> event.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="added"/> is null.</exception>
    public Document(DocumentAdded added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              new DocumentDescription(
                  added.Name,
                  added.Comments,
                  added.DocumentContainerId,
                  added.DocumentTypeId,
                  null),
              null,
              null,
              DocumentState.Create(DateTimeOffset.MinValue, string.Empty),
              [new DocumentActor(added.OwnerId, DocumentActorRole.Owner)],
              added.File,
              [],
              false)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.DocumentAggregateName;

    /// <inheritdoc/>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (domainEvent is DocumentEvent ev && domainEvent is not DocumentEnabled && Disabled)
        {
            return new ApplyResult(
                this,
                [new DocumentEventCancelled(ev, "Document is disabled.")],
                true);
        }

        return domainEvent switch
        {
            DocumentActorAdded e => ApplyEvent(e),
            DocumentActorRemoved e => ApplyEvent(e),
            DocumentAdded e => ApplyEvent(e),
            DocumentDescriptionChanged e => DocumentDescription.ApplyEvent(this, e),
            DocumentDisabled e => ApplyEvent(e),
            DocumentEnabled e => ApplyEvent(e),
            DocumentSummarized e => DocumentDescription.ApplyEvent(this, e),
            DocumentEvent e => new ApplyResult(
                this,
                [new DocumentEventCancelled(e, "Event not implemented")],
                true),
            _ => new ApplyResult(
                this,
                [InvalidEventApplied.CreateNotSupportedAppliedEvent(
                    AggregateName,
                    AggregateId,
                    domainEvent)],
                true),
        };
    }

    /// <inheritdoc/>
    public bool IsInitialized() => !string.IsNullOrWhiteSpace(Id);

    /// <summary>
    /// Applies a DocumentCreated event to the document.
    /// </summary>
    /// <param name="e">The DocumentCreated event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentAdded e) => !IsInitialized()
        ? new ApplyResult(
            new Document(e),
            [e],
            false)
        : new ApplyResult(this, [new DocumentEventCancelled(e, "The document already exists.")], true);

    /// <summary>
    /// Applies a DocumentEnabled event to the document.
    /// </summary>
    /// <param name="e">The DocumentEnabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new DocumentEventCancelled(e, "The document is already enabled.")], true);

    /// <summary>
    /// Applies a DocumentActorAdded event to the document.
    /// </summary>
    /// <param name="e">The DocumentActorAdded event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentActorAdded e) => new DocumentActors(Actors).ApplyEvent(this, e);

    /// <summary>
    /// Applies a DocumentActorRemoved event to the document.
    /// </summary>
    /// <param name="e">The DocumentActorRemoved event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentActorRemoved e) => new DocumentActors(Actors).ApplyEvent(this, e);

    /// <summary>
    /// Applies a DocumentDisabled event to the document.
    /// </summary>
    /// <param name="e">The DocumentDisabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new DocumentEventCancelled(e, "The document is already disabled.")], true);
}