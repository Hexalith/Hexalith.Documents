namespace Hexalith.Documents.Domain.DocumentContainers;

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Domain.Aggregates;
using Hexalith.Domain.Events;

/// <summary>
/// Represents a container for documents with associated metadata and behaviors.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="Name">The name of the document container.</param>
/// <param name="Description">The description of the document container.</param>
/// <param name="AutomaticRoutingInstructions">The instructions for automatic routing of documents.</param>
/// <param name="Actors">The collection of actors associated with the document container.</param>
/// <param name="FileTypeIds">The collection of file type identifiers supported by this container.</param>
/// <param name="Tags">The collection of tags associated with the document container.</param>
/// <param name="Disabled">A value indicating whether the document container is disabled.</param>
[DataContract]
public record DocumentContainer(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string DocumentPartitionId,
    [property: DataMember(Order = 3)] string Name,
    [property: DataMember(Order = 3)] string Path,
    [property: DataMember(Order = 4)] string? Description,
    [property: DataMember(Order = 5)] string? AutomaticRoutingInstructions,
    [property: DataMember(Order = 6)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 7)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 8)] IImmutableDictionary<string, string> Tags,
    [property: DataMember(Order = 9)] bool Disabled) : IDomainAggregate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContainer"/> class.
    /// </summary>
    public DocumentContainer()
        : this(
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              null,
              [],
              [],
              new Dictionary<string, string>().ToImmutableDictionary(),
              false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContainer"/> class.
    /// </summary>
    /// <param name="added">The creation event containing initial values.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="added"/> is null.</exception>
    public DocumentContainer(DocumentContainerCreated added)
        : this(
              (added ?? throw new ArgumentNullException(nameof(added))).Id,
              added.DocumentPartitionId,
              added.Name,
              added.Path,
              added.Description,
              null,
              [],
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
    public string AggregateName => DocumentDomainHelper.DocumentContainerAggregateName;

    /// <summary>
    /// Applies a domain event to the document type.
    /// </summary>
    /// <param name="domainEvent">The domain event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (domainEvent is DocumentContainerEvent ev && domainEvent is not DocumentContainerEnabled && Disabled)
        {
            return new ApplyResult(
                this,
                [new DocumentContainerEventCancelled(ev, $"Document container {Id}/{Name} is disabled.")],
                true);
        }

        return domainEvent switch
        {
            DocumentContainerCreated e => ApplyEvent(e),
            DocumentContainerActorAdded e => ApplyEvent(e),
            DocumentContainerActorRemoved e => ApplyEvent(e),
            DocumentContainerAutomaticRoutingInstructionsChanged e => ApplyEvent(e),
            DocumentContainerDescriptionChanged e => ApplyEvent(e),
            DocumentContainerDisabled e => ApplyEvent(e),
            DocumentContainerEnabled e => ApplyEvent(e),
            DocumentContainerFileTypeAdded e => ApplyEvent(e),
            DocumentContainerFileTypeRemoved e => ApplyEvent(e),
            DocumentContainerTagAdded e => ApplyEvent(e),
            DocumentContainerTagRemoved e => ApplyEvent(e),
            DocumentContainerEvent e => new ApplyResult(
                this,
                [new DocumentContainerEventCancelled(e, "Event not implemented")],
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
    private ApplyResult ApplyEvent(DocumentContainerCreated e) => !IsInitialized()
        ? new ApplyResult(
            new DocumentContainer(e),
            [e],
            false)
        : new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The document container {Id}/{Name} already exists.")], true);

    /// <summary>
    /// Applies a document type enable event.
    /// </summary>
    /// <param name="e">The enable event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerEnabled e) => Disabled
            ? new ApplyResult(
            this with { Disabled = false },
            [e],
            false)
            : new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The document container {Id}/{Name} is already enabled.")], true);

    /// <summary>
    /// Applies a document type disable event.
    /// </summary>
    /// <param name="e">The disable event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerDisabled e) => !Disabled
            ? new ApplyResult(
            this with { Disabled = true },
            [e],
            false)
            : new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The document container {Id}/{Name} is already disabled.")], true);

    /// <summary>
    /// Applies a document type description change event.
    /// </summary>
    /// <param name="e">The description change event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerDescriptionChanged e) => e.Name != Name || e.Description != Description
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
    private ApplyResult ApplyEvent(DocumentContainerFileTypeAdded e)
    {
        List<string> fileTypes = [.. FileTypeIds];
        if (fileTypes.Contains(e.FileTypeId))
        {
            return new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The file type {e.FileTypeId} already exists in document container {Id}/{Name}.")], true);
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
    private ApplyResult ApplyEvent(DocumentContainerFileTypeRemoved e)
    {
        List<string> fileTypes = [.. FileTypeIds];
        if (!fileTypes.Remove(e.FileTypeId))
        {
            return new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The file type {e.FileTypeId} does not exist in document container {Id}/{Name}.")], true);
        }

        return new ApplyResult(
            this with { FileTypeIds = fileTypes },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to add a tag.
    /// </summary>
    /// <param name="e">The tag addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerTagAdded e)
    {
        if (Tags.ContainsKey(e.TagId))
        {
            return new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The tag {e.TagId} already exists in document container {Id}/{Name}.")], true);
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
    private ApplyResult ApplyEvent(DocumentContainerTagRemoved e)
    {
        Dictionary<string, string> tags = Tags.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        if (!tags.Remove(e.TagId))
        {
            return new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The tag {e.TagId} does not exist in document container {Id}/{Name}.")], true);
        }

        return new ApplyResult(
            this with { Tags = tags.ToImmutableDictionary() },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to add an actor.
    /// </summary>
    /// <param name="e">The actor addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerActorAdded e)
    {
        List<DocumentActor> actors = [.. Actors];
        if (actors.Any(a => a.ContactId == e.Actor.ContactId))
        {
            return new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The actor {e.Actor.ContactId} already exists in document container {Id}/{Name}.")], true);
        }

        actors.Add(e.Actor);
        return new ApplyResult(
            this with { Actors = actors },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to remove an actor.
    /// </summary>
    /// <param name="e">The actor removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerActorRemoved e)
    {
        List<DocumentActor> actors = [.. Actors];
        if (actors.RemoveAll(a => a.ContactId == e.ContactId) == 0)
        {
            return new ApplyResult(this, [new DocumentContainerEventCancelled(e, $"The actor {e.ContactId} does not exist in document container {Id}/{Name}.")], true);
        }

        return new ApplyResult(
            this with { Actors = actors },
            [e],
            false);
    }

    /// <summary>
    /// Applies an event to change automatic routing instructions.
    /// </summary>
    /// <param name="e">The automatic routing instructions change event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerAutomaticRoutingInstructionsChanged e)
    {
        if (AutomaticRoutingInstructions == e.AutomaticRoutingInstructions)
        {
            return new ApplyResult(this, [], false);
        }

        return new ApplyResult(
            this with { AutomaticRoutingInstructions = e.AutomaticRoutingInstructions },
            [e],
            false);
    }
}