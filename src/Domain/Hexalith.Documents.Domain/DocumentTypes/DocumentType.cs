namespace Hexalith.Documents.Domain.DocumentTypes;

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Document.Domain;
using Hexalith.Documents.Events.DocumentTypes;
using Hexalith.Domain.Aggregates;
using Hexalith.Domain.Events;

/// <summary>
/// Represents a document type in the system, defining metadata and processing rules for documents.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Name">The name of the document type.</param>
/// <param name="Description">A detailed description of the document type.</param>
/// <param name="DataExtractionInstructions">A collection of instructions for extracting data from documents of this type.</param>
/// <param name="FileTypeIds">A collection of supported file type identifiers.</param>
/// <param name="Tags">A collection of tags associated with this document type.</param>
/// <param name="Disabled">A value indicating whether this document type is disabled.</param>
[DataContract]
public record DocumentType(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Description,
    [property: DataMember(Order = 4)] IImmutableDictionary<string, string> DataExtractionInstructions,
    [property: DataMember(Order = 5)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 6)] IImmutableDictionary<string, string> Tags,
    [property: DataMember(Order = 7)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentType"/> class.
    /// Creates a default document type with empty values.
    /// </summary>
    public DocumentType()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              new Dictionary<string, string>().ToImmutableDictionary(),
              [],
              new Dictionary<string, string>().ToImmutableDictionary(),
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentType"/> class based on a creation event.
    /// </summary>
    /// <param name="added">The event containing the initial document type data.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="added"/> is null.</exception>
    public DocumentType(DocumentTypeCreated added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.Name,
              added.Description,
              new Dictionary<string, string>().ToImmutableDictionary(),
              added.FileTypeIds,
              new Dictionary<string, string>().ToImmutableDictionary(),
              false)
    {
    }

    /// <summary>
    /// Gets the unique identifier of the aggregate.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the name of the aggregate type.
    /// </summary>
    public string AggregateName => DocumentDomainHelper.DocumentAggregateName;

    /// <summary>
    /// Applies a domain event to the document type.
    /// </summary>
    /// <param name="domainEvent">The domain event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (domainEvent is DocumentTypeEvent ev && domainEvent is not DocumentTypeEnabled && Disabled)
        {
            return new ApplyResult(
                this,
                [new DocumentTypeEventCancelled(ev, "Document is disabled.")],
                true);
        }

        return domainEvent switch
        {
            DocumentTypeCreated e => ApplyEvent(e),
            DocumentTypeDataExtractionAdded e => ApplyEvent(e),
            DocumentTypeDataExtractionRemoved e => ApplyEvent(e),
            DocumentTypeDescriptionChanged e => ApplyEvent(e),
            DocumentTypeDisabled e => ApplyEvent(e),
            DocumentTypeEnabled e => ApplyEvent(e),
            DocumentTypeFileTypeAdded e => ApplyEvent(e),
            DocumentTypeFileTypeRemoved e => ApplyEvent(e),
            DocumentTypeTagAdded e => ApplyEvent(e),
            DocumentTypeTagRemoved e => ApplyEvent(e),
            DocumentTypeEvent e => new ApplyResult(
                this,
                [new DocumentTypeEventCancelled(e, "Event not implemented")],
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

    /// <summary>
    /// Determines whether the document type has been initialized with a valid identifier.
    /// </summary>
    /// <returns>true if the document type has a non-empty identifier; otherwise, false.</returns>
    public bool IsInitialized() => !string.IsNullOrWhiteSpace(Id);

    /// <summary>
    /// Applies a document type creation event.
    /// </summary>
    /// <param name="e">The creation event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeCreated e) => !IsInitialized()
        ? new ApplyResult(
            new DocumentType(e),
            [e],
            false)
        : new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The document already exists.")], true);

    /// <summary>
    /// Applies a document type enable event.
    /// </summary>
    /// <param name="e">The enable event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The document is already enabled.")], true);

    /// <summary>
    /// Applies a document type disable event.
    /// </summary>
    /// <param name="e">The disable event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The document is already disabled.")], true);

    /// <summary>
    /// Applies a document type description change event.
    /// </summary>
    /// <param name="e">The description change event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeDescriptionChanged e) => e.Name != Name || e.Description != Description
        ? new ApplyResult(
            this with { Name = e.Name, Description = e.Description },
            [e],
            false)
        : new ApplyResult(this, [], false);

    /// <summary>
    /// Applies an event to add a file type.
    /// </summary>
    /// <param name="e">The file type addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeFileTypeAdded e)
    {
        List<string> fileTypes = FileTypeIds.ToList();
        if (fileTypes.Contains(e.FileTypeId))
        {
            return new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The file type already exists.")], true);
        }

        fileTypes.Add(e.FileTypeId);
        return new ApplyResult(
            this with { FileTypeIds = fileTypes },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to remove a file type.
    /// </summary>
    /// <param name="e">The file type removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeFileTypeRemoved e)
    {
        List<string> fileTypes = FileTypeIds.ToList();
        if (!fileTypes.Remove(e.FileTypeId))
        {
            return new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The file type does not exist.")], true);
        }

        return new ApplyResult(
            this with { FileTypeIds = fileTypes },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to add a data extraction instruction.
    /// </summary>
    /// <param name="e">The data extraction addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeDataExtractionAdded e)
    {
        if (DataExtractionInstructions.ContainsKey(e.ExtractionId))
        {
            return new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The data extraction instruction already exists.")], true);
        }

        Dictionary<string, string> instructions = DataExtractionInstructions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        instructions[e.ExtractionId] = e.DataExtractionInstructions ?? string.Empty;

        return new ApplyResult(
            this with { DataExtractionInstructions = instructions.ToImmutableDictionary() },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to remove a data extraction instruction.
    /// </summary>
    /// <param name="e">The data extraction removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeDataExtractionRemoved e)
    {
        Dictionary<string, string> instructions = DataExtractionInstructions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        if (!instructions.Remove(e.ExtractionId))
        {
            return new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The data extraction instruction does not exist.")], true);
        }

        return new ApplyResult(
            this with { DataExtractionInstructions = instructions.ToImmutableDictionary() },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to add a tag.
    /// </summary>
    /// <param name="e">The tag addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeTagAdded e)
    {
        if (Tags.ContainsKey(e.TagId))
        {
            return new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The tag already exists.")], true);
        }

        Dictionary<string, string> tags = Tags.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        tags[e.TagId] = e.TagValue;

        return new ApplyResult(
            this with { Tags = tags.ToImmutableDictionary() },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to remove a tag.
    /// </summary>
    /// <param name="e">The tag removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentTypeTagRemoved e)
    {
        Dictionary<string, string> tags = Tags.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        if (!tags.Remove(e.TagId))
        {
            return new ApplyResult(this, [new DocumentTypeEventCancelled(e, "The tag does not exist.")], true);
        }

        return new ApplyResult(
            this with { Tags = tags.ToImmutableDictionary() },
            [e],
            false);
    }
}