// <copyright file="DocumentContainer.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.DocumentContainers;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.Documents.Events.DocumentContainers;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains;
using Hexalith.Domains.Results;

/// <summary>
/// Represents a container for documents with associated metadata and behaviors.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="DocumentStorageId">The identifier of the document storage associated with this container.</param>
/// <param name="Name">The name of the document container.</param>
/// <param name="Path">The path where documents are stored in the document storage.</param>
/// <param name="Comments">The description of the document container.</param>
/// <param name="AutomaticRoutingInstructions">The instructions for automatic routing of documents.</param>
/// <param name="Actors">The collection of actors associated with the document container.</param>
/// <param name="DocumentTypeIds">The collection of document types identifiers supported by this container.</param>
/// <param name="Tags">The collection of tags associated with the document container.</param>
/// <param name="Disabled">A value indicating whether the document container is disabled.</param>
[DataContract]
public record DocumentContainer(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string DocumentStorageId,
    [property: DataMember(Order = 3)] string Name,
    [property: DataMember(Order = 4)] string Path,
    [property: DataMember(Order = 5)] string? Comments,
    [property: DataMember(Order = 6)] string? AutomaticRoutingInstructions,
    [property: DataMember(Order = 7)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 8)] IEnumerable<string> DocumentTypeIds,
    [property: DataMember(Order = 9)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 10)] bool Disabled) : IDomainAggregate
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
              [],
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
              added.DocumentStorageId,
              added.Name,
              added.Path,
              added.Comments,
              null,
              [],
              [],
              [],
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
    /// Applies a domain event to the document container aggregate.
    /// </summary>
    /// <param name="domainEvent">The domain event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events, or an error if the event cannot be applied.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (Disabled && domainEvent is not DocumentContainerEnabled and not DocumentContainerDisabled)
        {
            return ApplyResult.Error(this, "Operation not allowed: The document container is currently disabled. Enable the container before making changes.");
        }

        if (!(this as IDomainAggregate).IsInitialized() && domainEvent is not DocumentContainerCreated)
        {
            return ApplyResult.Error(this, "Operation not allowed: The document container has not been initialized. Create the container first before applying changes.");
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
            DocumentContainerDocumentTypeAdded e => ApplyEvent(e),
            DocumentContainerDocumentTypeRemoved e => ApplyEvent(e),
            DocumentContainerTagAdded e => ApplyEvent(e),
            DocumentContainerTagRemoved e => ApplyEvent(e),
            DocumentContainerEvent => ApplyResult.NotImplemented(this),
            _ => ApplyResult.InvalidEvent(this, domainEvent),
        };
    }

    /// <summary>
    /// Applies a document container creation event.
    /// </summary>
    /// <param name="e">The creation event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerCreated e) => !(this as IDomainAggregate).IsInitialized()
        ? ApplyResult.Success(new DocumentContainer(e), [e])
        : ApplyResult.Error(this, "Creation failed: A document container with this ID already exists. Use a unique identifier to create a new container.");

    /// <summary>
    /// Applies an event to enable the document container.
    /// </summary>
    /// <param name="e">The enable event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerEnabled e) => Disabled
            ? ApplyResult.Success(this with { Disabled = false }, [e])
            : ApplyResult.Error(this, "Enable operation failed: The document container is already in an enabled state.");

    /// <summary>
    /// Applies an event to disable the document container.
    /// </summary>
    /// <param name="e">The disable event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerDisabled e) => !Disabled
            ? ApplyResult.Success(this with { Disabled = true }, [e])
            : ApplyResult.Error(this, "Disable operation failed: The document container is already in a disabled state.");

    /// <summary>
    /// Applies an event to change the document container's name and description.
    /// </summary>
    /// <param name="e">The description change event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerDescriptionChanged e) => e.Name != Name || e.Comments != Comments
        ? ApplyResult.Success(this with { Name = e.Name, Comments = e.Comments }, [e])
        : ApplyResult.Error(this, "Update failed: The provided name and description are identical to the current values. No changes needed.");

    /// <summary>
    /// Applies an event to add a document type to the container.
    /// </summary>
    /// <param name="e">The document type addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerDocumentTypeAdded e)
    {
        List<string> currentTargets = [.. DocumentTypeIds];
        return !currentTargets.Contains(e.DocumentTypeId)
            ? ApplyResult.Success(this with { DocumentTypeIds = currentTargets.Concat([e.DocumentTypeId]) }, [e])
            : ApplyResult.Error(this, $"Add document type failed: The document type '{e.DocumentTypeId}' is already associated with this container.");
    }

    /// <summary>
    /// Applies an event to remove a document type from the container.
    /// </summary>
    /// <param name="e">The document type removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerDocumentTypeRemoved e)
    {
        List<string> currentTargets = [.. DocumentTypeIds];
        return currentTargets.Contains(e.DocumentTypeId)
            ? ApplyResult.Success(this with { DocumentTypeIds = currentTargets.Where(t => t != e.DocumentTypeId) }, [e])
            : ApplyResult.Error(this, $"Remove document type failed: The document type '{e.DocumentTypeId}' is not associated with this container.");
    }

    /// <summary>
    /// Applies an event to add a tag to the document container.
    /// </summary>
    /// <param name="e">The tag addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerTagAdded e)
    {
        if (Tags.Any(p => p.Key == e.Key && p.Value == e.Value))
        {
            return ApplyResult.Error(this, $"Add tag failed: The tag '{e.Key}={e.Value}' already exists in this container.");
        }

        if (Tags.Any(p => p.Key == e.Key && (e.Unique || p.Unique)))
        {
            return ApplyResult.Error(this, $"Add tag failed: A unique tag with key '{e.Key}' already exists in this container. Remove the existing tag first.");
        }

        return ApplyResult.Success(
            this with
            {
                Tags = [..Tags
                    .Append(new DocumentTag(e.Key, e.Value, e.Unique))
                    .Distinct()
                    .OrderBy(p => p.Key)
                    .ThenBy(p => p.Value)],
            },
            [e]);
    }

    /// <summary>
    /// Applies an event to remove a tag from the document container.
    /// </summary>
    /// <param name="e">The tag removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerTagRemoved e)
    {
        if (!Tags.Any(p => p.Key == e.Key && p.Value == e.Value))
        {
            return ApplyResult.Error(this, $"Remove tag failed: The tag '{e.Key}={e.Value}' does not exist in this container.");
        }

        return ApplyResult.Success(
            this with { Tags = [.. Tags.Where(p => p.Key != e.Key || p.Value != e.Value)] },
            [e]);
    }

    /// <summary>
    /// Applies an event to add an actor to the document container.
    /// </summary>
    /// <param name="e">The actor addition event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerActorAdded e)
    {
        List<DocumentActor> actors = [.. Actors];
        if (actors.Any(a => a.ContactId == e.Actor.ContactId))
        {
            return ApplyResult.Error(this, $"Add actor failed: An actor with contact ID '{e.Actor.ContactId}' is already associated with this container.");
        }

        actors.Add(e.Actor);
        return ApplyResult.Success(
            this with { Actors = actors },
            [e]);
    }

    /// <summary>
    /// Applies an event to remove an actor from the document container.
    /// </summary>
    /// <param name="e">The actor removal event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerActorRemoved e)
    {
        List<DocumentActor> actors = [.. Actors];
        if (actors.RemoveAll(a => a.ContactId == e.ContactId) == 0)
        {
            return ApplyResult.Error(this, $"Remove actor failed: No actor with contact ID '{e.ContactId}' exists in this container.");
        }

        return ApplyResult.Success(
            this with { Actors = actors },
            [e]);
    }

    /// <summary>
    /// Applies an event to change the automatic routing instructions of the document container.
    /// </summary>
    /// <param name="e">The routing instructions change event to apply.</param>
    /// <returns>An <see cref="ApplyResult"/> containing the updated state and any resulting events.</returns>
    private ApplyResult ApplyEvent(DocumentContainerAutomaticRoutingInstructionsChanged e)
    {
        if (AutomaticRoutingInstructions == e.AutomaticRoutingInstructions)
        {
            return ApplyResult.Error(this, "Update failed: The provided automatic routing instructions are identical to the current values. No changes needed.");
        }

        return ApplyResult.Success(
            this with { AutomaticRoutingInstructions = e.AutomaticRoutingInstructions },
            [e]);
    }
}