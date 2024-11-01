namespace Hexalith.Documents.Domain.Documents;

using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Represents the routing information for a document, defining the sender, recipient, and copy recipients.
/// </summary>
[DataContract]
public record DocumentRouting(
    /// <summary>
    /// Gets the identifier of the contact sending the document.
    /// </summary>
    [property:DataMember(Order = 1)]
    string FromContactId,

    /// <summary>
    /// Gets the identifier of the contact receiving the document.
    /// </summary>
    [property: DataMember(Order = 2)]
    string ToContactId,

    /// <summary>
    /// Gets the collection of contact identifiers who should receive a copy of the document.
    /// </summary>
    [property: DataMember(Order = 3)]
    IEnumerable<string> CopyToContactId)
{
}
