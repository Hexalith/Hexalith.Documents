// <copyright file="Document.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Documents;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains;
using Hexalith.Domains.Results;

/// <summary>
/// Represents a document in the domain.
/// </summary>
/// <param name="Id">The ID of the document.</param>
/// <param name="Description">The description of the document.</param>
/// <param name="Routing">The routing of the document.</param>
/// <param name="ParentDocumentId">The ID of the parent document.</param>
/// <param name="State">The state of the document.</param>
/// <param name="Actors">The actors of the document.</param>
/// <param name="Files">The file of the document.</param>
/// <param name="Tags">The tags of the document.</param>
/// <param name="AccessKeys">The access keys of the document.</param>
/// <param name="Disabled">Indicates whether the document is disabled.</param>
[DataContract]
public record Document(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] DocumentDescription Description,
    [property: DataMember(Order = 3)] DocumentRouting? Routing,
    [property: DataMember(Order = 4)] string? ParentDocumentId,
    [property: DataMember(Order = 5)] DocumentState State,
    [property: DataMember(Order = 6)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 7)] IEnumerable<FileDescription> Files,
    [property: DataMember(Order = 8)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 8)] IEnumerable<DocumentAccessKey> AccessKeys,
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
              [],
              [],
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
              added.ParentDocumentId,
              DocumentState.Create(DateTimeOffset.MinValue, string.Empty),
              [new DocumentActor(added.OwnerId, DocumentActorRole.Owner)],
              added.Files,
              added.Tags,
              [],
              false)
    {
    }

    /// <inheritdoc/>
    public string AggregateId => Id;

    /// <inheritdoc/>
    public string AggregateName => DocumentDomainHelper.DocumentAggregateName;

    /// <inheritdoc/>
    [SuppressMessage("Critical Code Smell", "S1541:Methods and properties should not be too complex", Justification = "This method has no complexities")]
    public ApplyResult Apply([NotNull] object domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        if (Disabled && domainEvent is not DocumentEnabled and not DocumentDisabled)
        {
            return ApplyResult.Error(this, "Operation not allowed: The document is currently disabled. Enable the document before making changes.");
        }

        if (!(this as IDomainAggregate).IsInitialized() && domainEvent is not DocumentAdded)
        {
            return ApplyResult.Error(this, "Operation not allowed: The document has not been initialized. Create the document first before applying changes.");
        }

        return domainEvent switch
        {
            DocumentActorAdded e => ApplyEvent(e),
            DocumentActorRemoved e => ApplyEvent(e),
            DocumentAdded e => ApplyEvent(e),
            DocumentDescriptionChanged e => DocumentDescription.ApplyEvent(this, e),
            DocumentDisabled e => ApplyEvent(e),
            DocumentEnabled e => ApplyEvent(e),
            DocumentAccessKeyAdded e => ApplyEvent(e),
            DocumentSummarized e => DocumentDescription.ApplyEvent(this, e),
            DocumentEvent => ApplyResult.NotImplemented(this),
            _ => ApplyResult.InvalidEvent(this, domainEvent),
        };
    }

    private ApplyResult ApplyEvent(DocumentAccessKeyAdded e)
    {
        if (AccessKeys.Any(p => p.Key == e.AccessKey.Key))
        {
            return ApplyResult.Error(this, $"Add access key failed: The Key '{e.AccessKey.Key}' already exists for this document.");
        }

        return ApplyResult.Success(
            this with
            {
                AccessKeys = [..AccessKeys
                    .Append(e.AccessKey)
                    .Distinct()
                    .OrderByDescending(p => p.ValidUntil)],
            },
            [e]);
    }

    /// <summary>
    /// Applies a DocumentCreated event to the document.
    /// </summary>
    /// <param name="e">The DocumentCreated event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentAdded e) => !(this as IDomainAggregate).IsInitialized()
        ? ApplyResult.Success(new Document(e), [e])
        : ApplyResult.Error(this, "Creation failed: A document with this ID already exists. Use a unique identifier to create a new document.");

    /// <summary>
    /// Applies a DocumentEnabled event to the document.
    /// </summary>
    /// <param name="e">The DocumentEnabled event to apply.</param>
    /// <returns>The result of applying the event.</returns>
    private ApplyResult ApplyEvent(DocumentEnabled e) => Disabled
            ? ApplyResult.Success(this with { Disabled = false }, [e])
            : ApplyResult.Error(this, "Enable operation failed: The document is already in an enabled state.");

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
            ? ApplyResult.Success(this with { Disabled = true }, [e])
            : ApplyResult.Error(this, "Disable operation failed: The document is already in a disabled state.");
}