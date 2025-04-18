namespace Hexalith.Documents.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents an actor (user or system) that has specific permissions and responsibilities in relation to a document.
/// This value object encapsulates the identity and access level of entities interacting with documents in the system.
/// </summary>
/// <param name="ContactId">The unique contact identifier representing the actor in the system. This could be a user ID, system ID, or any other identifier that uniquely identifies the actor.</param>
/// <param name="Role">The role or permission level of the actor, defining their access rights and capabilities regarding the document. See <see cref="DocumentActorRole"/> for available roles.</param>
/// <remarks>
/// <para>
/// DocumentActor is a crucial part of the document access control system, combining identity (ContactId) with permissions (Role).
/// It is used throughout the document management system to:
/// - Control access to document operations
/// - Track document modifications
/// - Manage document ownership
/// - Enforce role-based permissions.
/// </para>
/// <para>
/// The default constructor creates a read-only actor with an empty contact ID, typically used as a placeholder
/// or when initializing new document-related entities.
/// </para>
/// </remarks>
[DataContract]
public record DocumentActor([property: DataMember(Order = 1)] string ContactId, [property: DataMember(Order = 2)] DocumentActorRole Role)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentActor"/> class.
    /// </summary>
    public DocumentActor()
        : this(string.Empty, DocumentActorRole.Reader)
    {
    }
}