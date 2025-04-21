// <copyright file="DocumentActors.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Documents;

using System.Collections.Generic;
using System.Linq;

using Hexalith.Documents.Events.Documents;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains.Results;

/// <summary>
/// Represents a collection of document actors and provides methods to manage them.
/// </summary>
/// <param name="actors">The immutable collection of document actors that defines the roles and permissions for each actor in the document.</param>
public class DocumentActors(IEnumerable<DocumentActor> actors)
{
    /// <summary>
    /// Finds an actor by their contact ID.
    /// </summary>
    /// <param name="contactId">The contact ID to search for.</param>
    /// <returns>The found document actor, or null if not found.</returns>
    public DocumentActor? FindActor(string contactId)
        => actors.FirstOrDefault(a => a.ContactId == contactId);

    /// <summary>
    /// Applies a DocumentActorAdded event to the document.
    /// </summary>
    /// <param name="document">The document to apply the event to.</param>
    /// <param name="e">The DocumentActorAdded event.</param>
    /// <returns>An ApplyResult containing the updated document and any resulting events.</returns>
    /// <remarks>
    /// If the actor already exists, the event will be cancelled.
    /// Otherwise, the actor will be added with the Owner role.
    /// </remarks>
    internal ApplyResult ApplyEvent(Document document, DocumentActorAdded e)
    {
        DocumentActor? documentActor = FindActor(e.Actor.ContactId);
        if (documentActor != null)
        {
            return new(document, [new DocumentEventCancelled(e, $"Could not add the owner. The contact {e.Actor.ContactId} already exists in the document with role {documentActor.Role}")], true);
        }

        return new(
            document with
            {
                Actors = [.. actors
                    .Append(new(e.Actor.ContactId, DocumentActorRole.Owner))
                    .DistinctBy(x => x.ContactId)],
            },
            [e],
            false);
    }

    /// <summary>
    /// Applies a DocumentActorRemoved event to the document.
    /// </summary>
    /// <param name="document">The document to apply the event to.</param>
    /// <param name="e">The DocumentActorRemoved event.</param>
    /// <returns>An ApplyResult containing the updated document and any resulting events.</returns>
    /// <remarks>
    /// The event will be cancelled if:
    /// - The actor does not exist in the document
    /// - The actor is the last owner of the document.
    /// </remarks>
    internal ApplyResult ApplyEvent(Document document, DocumentActorRemoved e)
    {
        DocumentActor? documentActor = FindActor(e.ContactId);
        if (documentActor == null)
        {
            return new(document, [new DocumentEventCancelled(e, $"Could not remove the actor {e.ContactId}. The contact does not exist in the document")], true);
        }

        if (documentActor.Role == DocumentActorRole.Owner && actors.Count(p => p.Role == DocumentActorRole.Owner) < 2)
        {
            return new(document, [new DocumentEventCancelled(e, $"Could not remove the owner {e.ContactId}. The document must have at least one owner")], true);
        }

        return new(
            document with { Actors = [.. actors.Where(p => p.ContactId != e.ContactId)] },
            [e],
            false);
    }
}