namespace Hexalith.Documents.Domain.Documents;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Document.Domain;
using Hexalith.Document.Domain.ValueObjects;
using Hexalith.Documents.Domain;
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
    /// Gets the name of the document.
    /// </summary>
    /// <value>The document's display name.</value>
    [property: DataMember(Order = 2)] string Name,

    /// <summary>
    /// Gets the detailed description of the document.
    /// </summary>
    /// <value>The document's description text.</value>
    [property: DataMember(Order = 3)] string Description,

    /// <summary>
    /// Gets the URL where the document can be accessed.
    /// </summary>
    /// <value>The document's location URL.</value>
    [property: DataMember(Order = 4)] Uri LocationUrl,
    /// <summary>
    /// Gets the type identifier of the document.
    /// </summary>
    /// <value>The document's type identifier string.</value>
    [property: DataMember(Order = 5)] string? DocumentTypeId,
    [property: DataMember(Order = 6)] string? Summary,

    [property: DataMember(Order = 7)] DocumentStatus Status,

    [property: DataMember(Order = 8)] IEnumerable<DocumentActor> Actors,

    [property: DataMember(Order = 9)] IEnumerable<string> Tags,

    [property: DataMember(Order = 10)] DateTimeOffset CreatedOn,
    [property: DataMember(Order = 11)] string CreatedBy,
    [property: DataMember(Order = 12)] DateTimeOffset? ModifiedOn,
    [property: DataMember(Order = 13)] string? ModifiedBy,
    [property: DataMember(Order = 14)] DateTimeOffset? ValidatedOn,
    [property: DataMember(Order = 15)] string? ValidatedBy,
    [property: DataMember(Order = 16)] DateTimeOffset? PublishedOn,
    [property: DataMember(Order = 17)] string? PublishedBy,

    /// <summary>
    /// Gets a value indicating whether the document is disabled.
    /// </summary>
    [property: DataMember(Order = 6)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class.
    /// </summary>
    public Document()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              new Uri("empty.undefined.error"),
              null,
              null,
              DocumentStatus.Draft,
              [],
              [],
              DateTimeOffset.MinValue,
              string.Empty,
              null,
              null,
              null,
              null,
              null,
              null,
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class based on the <see cref="DocumentCreated"/> event.
    /// </summary>
    /// <param name="added">The <see cref="DocumentCreated"/> event.</param>
    public Document(DocumentCreated added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.Description,
              added.LocationUrl,
              added.DocumentTypeId,
              null,
              DocumentStatus.Draft,
              [new DocumentActor(added.OwnerId, DocumentActorRole.Owner)],
              [],
              added.CreatedOn,
              string.Empty,
              null,
              null,
              null,
              null,
              null,
              null,
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
            DocumentDescriptionChanged e => ApplyEvent(e),
            DocumentDisabled e => ApplyEvent(e),
            DocumentEnabled e => ApplyEvent(e),
            DocumentSummarized e => ApplyEvent(e),
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

    private ApplyResult ApplyEvent(DocumentCreated e) => !IsInitialized()
        ? new ApplyResult(
            new Document(e),
            [e],
            false)
        : new ApplyResult(this, [new DocumentEventCancelled(e, "The document already exists.")], true);

    private ApplyResult ApplyEvent(DocumentEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new DocumentEventCancelled(e, "The document is already enabled.")], true);

    private ApplyResult ApplyEvent(DocumentActorAdded e) => new DocumentActors(Actors).ApplyEvent(this, e);

    private ApplyResult ApplyEvent(DocumentActorRemoved e) => new DocumentActors(Actors).ApplyEvent(this, e);

    private ApplyResult ApplyEvent(DocumentDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new DocumentEventCancelled(e, "The document is already disabled.")], true);

    private ApplyResult ApplyEvent(DocumentSummarized e) => e.Summary != Summary
        ? new ApplyResult(
            this with { Summary = e.Summary },
            [e],
            false)
        : new ApplyResult(this, [], false);

    private ApplyResult ApplyEvent(DocumentDescriptionChanged e) => e.Name != Name || e.Description != Description
        ? new ApplyResult(
            this with { Name = e.Name, Description = e.Description },
            [e],
            false)
        : new ApplyResult(this, [], false);
}