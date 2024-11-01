namespace Hexalith.Documents.Domain.Documents;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Document.Domain;
using Hexalith.Document.Domain.ValueObjects;
using Hexalith.Documents.Events;
using Hexalith.Domain.Aggregates;
using Hexalith.Domain.Events;

/// <summary>
/// Represents a document in the domain.
/// </summary>
[DataContract]
public record Document(
    /// <summary>
    /// Gets the unique identifier of the document.
    /// </summary>
    /// <value>The document's unique identifier string.</value>
    [property: DataMember(Order = 1)] string Id,

    /// <summary>
    /// Gets the descriptive information of the document.
    /// </summary>
    /// <value>The document's descriptive information.</value>
    [property: DataMember(Order = 2)] DocumentDescription Description,

    /// <summary>
    /// Gets the routing information for the document.
    /// </summary>
    /// <value>The document's routing configuration.</value>
    [property: DataMember(Order = 3)] DocumentRouting? Routing,

    /// <summary>
    /// Gets the identifier of the parent document.
    /// </summary>
    /// <value>The parent document's identifier string.</value>
    [property: DataMember(Order = 4)] string? ParentDocumentId,

    /// <summary>
    /// Gets the current state of the document.
    /// </summary>
    /// <value>The document's current state information.</value>
    [property: DataMember(Order = 5)] DocumentState State,

    /// <summary>
    /// Gets the collection of actors associated with the document.
    /// </summary>
    /// <value>The enumerable collection of document actors.</value>
    [property: DataMember(Order = 6)] IEnumerable<DocumentActor> Actors,

    /// <summary>
    /// Gets the file description associated with the document.
    /// </summary>
    /// <value>The document's file description.</value>
    [property: DataMember(Order = 7)] FileDescription File,

    /// <summary>
    /// Gets the collection of tags associated with the document.
    /// </summary>
    /// <value>The enumerable collection of tag strings.</value>
    [property: DataMember(Order = 8)] IEnumerable<string> Tags,

    /// <summary>
    /// Gets a value indicating whether the document is disabled.
    /// </summary>
    /// <value>True if the document is disabled; otherwise, false.</value>
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
                  string.Empty,
                  null,
                  null,
                  null),
              null,
              null,
              new DocumentState(DateTimeOffset.MinValue, string.Empty),
              [],
              new FileDescription(string.Empty, string.Empty, string.Empty, string.Empty),
              [],
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class based on the <see cref="DocumentCreated"/> event.
    /// </summary>
    /// <param name="added">The <see cref="DocumentCreated"/> event.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="added"/> is null.</exception>
    public Document(DocumentCreated added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              new DocumentDescription(
                  added.Name,
                  added.Description,
                  null,
                  added.DocumentTypeId,
                  null),
              null,
              null,
              new DocumentState(DateTimeOffset.MinValue, string.Empty),
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
            DocumentCreated e => ApplyEvent(e),
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
    private ApplyResult ApplyEvent(DocumentCreated e) => !IsInitialized()
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