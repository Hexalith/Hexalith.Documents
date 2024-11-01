namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a tag is added to a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="TagId">The identifier of the tag being added.</param>
/// <param name="TagValue">The value associated with the tag.</param>
[PolymorphicSerialization]
public partial record DocumentTypeTagAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string TagId,
    [property: DataMember(Order = 3)]
    string TagValue)
    : DocumentTypeEvent(Id)
{
}
