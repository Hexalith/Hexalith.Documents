namespace Hexalith.Documents.Domain.Documents;

using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Represents the routing information for a document, defining the sender, recipient, and copy recipients.
/// </summary>
/// <param name="FromContactId">The unique identifier of the sender.</param>
/// <param name="ToContactId">The unique identifier of the recipient.</param>
/// <param name="CopyToContactId">The collection of unique identifiers of the copy recipients.</param>
[DataContract]
public record DocumentRouting(
    [property:DataMember(Order = 1)]
    string FromContactId,
    [property: DataMember(Order = 2)]
    string ToContactId,
    [property: DataMember(Order = 3)]
    IEnumerable<string> CopyToContactId)
{
}