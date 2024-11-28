namespace Hexalith.Documents.Domain.Documents;

using System;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;

/// <summary>
/// Represents the immutable state of a document in the system, tracking its lifecycle from creation through publication.
/// </summary>
/// <param name="Revision">The revision number of the document.</param>
/// <param name="Status">The current status of the document.</param>
/// <param name="CreatedOn">The timestamp when the document was created.</param>
/// <param name="CreatedByContactId">The identifier of the contact who created the document.</param>
/// <param name="ModifiedOn">The timestamp when the document was last modified.</param>
/// <param name="ModifiedByContactId">The identifier of the contact who last modified the document.</param>
/// <param name="ValidatedOn">The timestamp when the document was validated.</param>
/// <param name="ValidatedByContactId">The identifier of the contact who validated the document.</param>
/// <param name="PublishedOn">The timestamp when the document was published.</param>
/// <param name="PublishedByContactId">The identifier of the contact who published the document.</param>
[DataContract]
public record DocumentState(
    [property: DataMember(Order = 1)] int Revision,
    [property: DataMember(Order = 2)] DocumentStatus Status,
    [property: DataMember(Order = 3)] DateTimeOffset CreatedOn,
    [property: DataMember(Order = 4)] string CreatedByContactId,
    [property: DataMember(Order = 5)] DateTimeOffset? ModifiedOn,
    [property: DataMember(Order = 6)] string? ModifiedByContactId,
    [property: DataMember(Order = 7)] DateTimeOffset? ValidatedOn,
    [property: DataMember(Order = 8)] string? ValidatedByContactId,
    [property: DataMember(Order = 9)] DateTimeOffset? PublishedOn,
    [property: DataMember(Order = 10)] string? PublishedByContactId)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentState"/> class in Draft status.
    /// </summary>
    /// <param name="createdOn">The creation timestamp of the document.</param>
    /// <param name="createdByContactId">The identifier of the contact who created the document.</param>
    /// <remarks>
    /// This constructor creates a new document in Draft status with only the creation information set.
    /// All other state fields (modification, validation, publication) are initialized to null.
    /// </remarks>
    public DocumentState(DateTimeOffset createdOn, string createdByContactId)
        : this(
            0,
            DocumentStatus.Draft,
            createdOn,
            createdByContactId,
            null,
            null,
            null,
            null,
            null,
            null)
    {
    }
}