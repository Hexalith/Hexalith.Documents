namespace Hexalith.Documents.Documents;

using System;
using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;

/// <summary>
/// Represents the immutable state of a document in the system, tracking its lifecycle from creation through publication.
/// </summary>
/// <param name="Revision">The revision number of the document.</param>
/// <param name="Status">The current status of the document.</param>
/// <param name="CreatedOn">The timestamp when the document was created.</param>
/// <param name="CreatedById">The identifier of the contact who created the document.</param>
/// <param name="ModifiedOn">The timestamp when the document was last modified.</param>
/// <param name="ModifiedById">The identifier of the contact who last modified the document.</param>
/// <param name="ValidatedOn">The timestamp when the document was validated.</param>
/// <param name="ValidatedById">The identifier of the contact who validated the document.</param>
/// <param name="PublishedOn">The timestamp when the document was published.</param>
/// <param name="PublishedById">The identifier of the contact who published the document.</param>
[DataContract]
public record DocumentState(
    [property: DataMember(Order = 1)] int Revision,
    [property: DataMember(Order = 2)] DocumentStatus Status,
    [property: DataMember(Order = 3)] DateTimeOffset CreatedOn,
    [property: DataMember(Order = 4)] string CreatedById,
    [property: DataMember(Order = 5)] DateTimeOffset? ModifiedOn,
    [property: DataMember(Order = 6)] string? ModifiedById,
    [property: DataMember(Order = 7)] DateTimeOffset? ValidatedOn,
    [property: DataMember(Order = 8)] string? ValidatedById,
    [property: DataMember(Order = 9)] DateTimeOffset? PublishedOn,
    [property: DataMember(Order = 10)] string? PublishedById)
{
    /// <summary>
    /// Creates a new instance of the <see cref="DocumentState"/> class with the specified creation timestamp and creator contact ID.
    /// </summary>
    /// <param name="createdOn">The timestamp when the document was created.</param>
    /// <param name="createdByContactId">The identifier of the contact who created the document.</param>
    /// <returns>A new instance of the <see cref="DocumentState"/> class.</returns>
    public static DocumentState Create(DateTimeOffset createdOn, string createdByContactId)
        => new(
            0,
            DocumentStatus.Draft,
            createdOn,
            createdByContactId,
            null,
            null,
            null,
            null,
            null,
            null);
}