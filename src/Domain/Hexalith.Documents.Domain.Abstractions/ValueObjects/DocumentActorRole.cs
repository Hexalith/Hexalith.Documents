namespace Hexalith.Documents.Domain.ValueObjects;

/// <summary>
/// Represents the role or permission level that an actor (user or system) has in relation to a document.
/// </summary>
public enum DocumentActorRole
{
    /// <summary>
    /// Indicates the actor has full control over the document, including management of access rights.
    /// </summary>
    Owner,

    /// <summary>
    /// Indicates the actor has read-only access to view the document content.
    /// </summary>
    Reader,

    /// <summary>
    /// Indicates the actor can make modifications to the document content.
    /// </summary>
    Contributor,
}