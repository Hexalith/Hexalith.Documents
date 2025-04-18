namespace Hexalith.Documents.ValueObjects;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the current status of a document in its lifecycle, defining its state and available operations.
/// Each status represents a distinct phase in the document's lifecycle with specific rules and permissions.
/// </summary>
/// <remarks>
/// <para>
/// The document lifecycle follows a progressive flow:
/// Draft -> Final -> Validated -> Published.
/// </para>
/// <para>
/// Status transitions are typically restricted to follow this progression, with specific rules:
/// - Only authorized actors can initiate status changes
/// - Some transitions may require additional validation or approval
/// - Each status change is tracked for audit purposes.
/// </para>
/// <para>
/// The status affects what operations are allowed on the document and who can perform them,
/// working in conjunction with <see cref="DocumentActor"/> roles to enforce access control.
/// </para>
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentStatus
{
    /// <summary>
    /// The document is in draft state and can be modified freely.
    /// This is the initial status of newly created documents.
    /// </summary>
    /// <remarks>
    /// In Draft status:
    /// - Content can be modified by Contributors and Owners
    /// - Document is not visible to general users
    /// - Metadata can be updated
    /// - Document can be deleted
    /// - Can be promoted to Final status when ready.
    /// </remarks>
    Draft = 0,

    /// <summary>
    /// The document is finalized but awaiting validation.
    /// This status indicates the document content is complete but requires review.
    /// </summary>
    /// <remarks>
    /// In Final status:
    /// - Content modifications are restricted
    /// - Only metadata updates are allowed
    /// - Document is visible to authorized reviewers
    /// - Can be returned to Draft if changes are needed
    /// - Can be promoted to Validated status after review.
    /// </remarks>
    Final = 1,

    /// <summary>
    /// The document has been reviewed and validated by an authorized person.
    /// This status confirms the document's content meets required standards.
    /// </summary>
    /// <remarks>
    /// In Validated status:
    /// - Content is locked from modifications
    /// - Only specific metadata updates allowed
    /// - Document is ready for publication
    /// - Requires Owner permission for status changes
    /// - Can be promoted to Published status.
    /// </remarks>
    Validated = 2,

    /// <summary>
    /// The document has been published and is available for general access.
    /// This is the final status in the standard document lifecycle.
    /// </summary>
    /// <remarks>
    /// In Published status:
    /// - Document is publicly accessible (subject to access controls)
    /// - Content and core metadata are immutable
    /// - Only usage metadata can be updated
    /// - New versions must be created for changes
    /// - Archival policies may apply.
    /// </remarks>
    Published = 3,
}