namespace Hexalith.Documents.ValueObjects;

/// <summary>
/// Defines the hierarchical roles and permission levels that actors can have in relation to documents.
/// These roles determine what operations an actor can perform on documents in the system.
/// </summary>
/// <remarks>
/// <para>
/// The roles follow a hierarchical structure where each level includes specific permissions:
/// - Owner: Has full control, including permission management
/// - Contributor: Can modify content but cannot manage permissions
/// - Reader: Has read-only access.
/// </para>
/// <para>
/// These roles are used in conjunction with <see cref="DocumentActor"/> to implement
/// the document access control system and enforce security policies.
/// </para>
/// </remarks>
public enum DocumentActorRole
{
    /// <summary>
    /// Indicates the actor has full control over the document, including management of access rights.
    /// An owner can:
    /// - Read and modify document content
    /// - Delete the document
    /// - Grant or revoke access rights
    /// - Manage document metadata
    /// - Transfer ownership.
    /// </summary>
    Owner,

    /// <summary>
    /// Indicates the actor has read-only access to view the document content.
    /// A reader can:
    /// - View document content
    /// - Access document metadata
    /// - Download permitted content
    /// Cannot modify content or manage permissions.
    /// </summary>
    Reader,

    /// <summary>
    /// Indicates the actor can make modifications to the document content.
    /// A contributor can:
    /// - Read document content
    /// - Modify document content
    /// - Update document metadata
    /// Cannot manage access permissions or delete the document.
    /// </summary>
    Contributor,
}