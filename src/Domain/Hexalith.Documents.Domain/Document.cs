namespace Hexalith.Documents.Domain;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json;

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
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] Person Person,
    [property: DataMember(Order = 5)] IEnumerable<DocumentPoint> DocumentPoints,
    [property: DataMember(Order = 6)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class.
    /// </summary>
    public Document()
        : this(string.Empty, string.Empty, null, new Person(), [], false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class based on the <see cref="DocumentAdded"/> event.
    /// </summary>
    /// <param name="added">The <see cref="DocumentAdded"/> event.</param>
    public Document(DocumentAdded added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.Description,
              added.Person,
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
        if (domainEvent is DocumentAdded added)
        {
            if (!IsInitialized())
            {
                return ApplyEvent(added);
            }

            return new ApplyResult(
                this,
                [new DocumentEventCancelled(added, $"Aggregate {Id}/{Name} already initialized")],
                true);
        }

        if (domainEvent is DocumentEvent documentEvent)
        {
            if (documentEvent.AggregateId != AggregateId)
            {
                return new ApplyResult(this, [new DocumentEventCancelled(documentEvent, $"Invalid aggregate identifier for {Id}/{Name} : {documentEvent.AggregateId}")], true);
            }
        }
        else
        {
            return new ApplyResult(
                this,
                [new InvalidEventApplied(
                    AggregateName,
                    AggregateId,
                    domainEvent.GetType().FullName ?? "Unknown",
                    JsonSerializer.Serialize(domainEvent),
                    $"Unexpected event applied.")],
                true);
        }

        return documentEvent switch
        {
            DocumentPersonChanged e => ApplyEvent(e),
            DocumentDescriptionChanged e => ApplyEvent(e),
            DocumentDisabled e => ApplyEvent(e),
            DocumentEnabled e => ApplyEvent(e),
            DocumentPointAdded e => ApplyEvent(e),
            DocumentPointChanged e => ApplyEvent(e),
            DocumentPointRemoved e => ApplyEvent(e),
            _ => new ApplyResult(
                this,
                [new DocumentEventCancelled(documentEvent, "Event not implemented")],
                true),
        };
    }

    /// <inheritdoc/>
    public bool IsInitialized() => !string.IsNullOrWhiteSpace(Id);

    /// <summary>
    /// Applies the DocumentAdded event.
    /// </summary>
    /// <param name="e">The DocumentAdded event.</param>
    /// <returns>ApplyResult.</returns>
    private static ApplyResult ApplyEvent(DocumentAdded e) => new(
        new Document(e),
        [e],
        false);

    /// <summary>
    /// Applies the DocumentPointAdded event.
    /// </summary>
    /// <param name="e">The DocumentPointAdded event.</param>
    /// <returns>ApplyResult.</returns>
    private ApplyResult ApplyEvent(DocumentPointAdded e)
    {
        if (DocumentPoints.Any(p => p.Name == e.DocumentPoint.Name))
        {
            return new ApplyResult(this, [new DocumentEventCancelled(e, $"Document point {e.DocumentPoint.Name} already exists for {Id}/{Name}")], true);
        }

        return new ApplyResult(
            this with { DocumentPoints = DocumentPoints.Union([e.DocumentPoint]).OrderBy(p => p.Name).ToList() },
            [e],
            false);
    }

    /// <summary>
    /// Applies the DocumentPointChanged event.
    /// </summary>
    /// <param name="e">The DocumentPointChanged event.</param>
    /// <returns>ApplyResult.</returns>
    private ApplyResult ApplyEvent(DocumentPointChanged e)
    {
        List<DocumentPoint> points = DocumentPoints.ToList();
        DocumentPoint? oldValue = points.FirstOrDefault(p => p.Name == e.DocumentPoint.Name);
        if (oldValue == null)
        {
            return new ApplyResult(this, [new DocumentEventCancelled(e, $"Document point {e.DocumentPoint.Name} does not exist for {Id}/{Name}")], true);
        }

        if (oldValue != e.DocumentPoint)
        {
            return new ApplyResult(
                this with { DocumentPoints = points.Where(p => p.Name != e.DocumentPoint.Name).Union([e.DocumentPoint]).OrderBy(p => p.Name).ToList() },
                [e],
                false);
        }

        return new ApplyResult(this, [], false);
    }

    /// <summary>
    /// Applies the DocumentPointRemoved event.
    /// </summary>
    /// <param name="e">The DocumentPointRemoved event.</param>
    /// <returns>ApplyResult.</returns>
    private ApplyResult ApplyEvent(DocumentPointRemoved e)
    {
        if (DocumentPoints.Any(p => p.Name == e.Name) == false)
        {
            return new ApplyResult(this, [new DocumentEventCancelled(e, $"Document point {e.Name} does not exist for {Id}/{Name}")], true);
        }

        return new ApplyResult(
            this with { DocumentPoints = DocumentPoints.Where(p => p.Name != e.Name).ToList() },
            [e],
            false);
    }

    /// <summary>
    /// Applies the DocumentDescriptionChanged event.
    /// </summary>
    /// <param name="e">The DocumentDescriptionChanged event.</param>
    /// <returns>ApplyResult.</returns>
    private ApplyResult ApplyEvent(DocumentDescriptionChanged e) => Comments == e.Comments && Name == e.Name
            ? new ApplyResult(this, [], true)
            : new ApplyResult(
            this with { Comments = e.Comments, Name = e.Name },
            [e],
            false);

    /// <summary>
    /// Applies the DocumentDisabled event.
    /// </summary>
    /// <param name="e">The DocumentDisabled event.</param>
    /// <returns>ApplyResult.</returns>
    private ApplyResult ApplyEvent(DocumentDisabled e) => Disabled
            ? new ApplyResult(this, [], true)
            : new ApplyResult(
            this with { Disabled = true },
            [e],
            false);

    /// <summary>
    /// Applies the DocumentPersonChanged event.
    /// </summary>
    /// <param name="e">The DocumentPersonChanged event.</param>
    /// <returns>ApplyResult.</returns>
    private ApplyResult ApplyEvent(DocumentPersonChanged e) => new(
            this with { Person = e.Person },
            [e],
            false);

    /// <summary>
    /// Applies the DocumentEnabled event.
    /// </summary>
    /// <param name="e">The DocumentEnabled event.</param>
    /// <returns>ApplyResult.</returns>
    private ApplyResult ApplyEvent(DocumentEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [], true);
}