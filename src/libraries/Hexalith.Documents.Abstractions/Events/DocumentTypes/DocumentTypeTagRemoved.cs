namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a tag is removed from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Key">The identifier of the tag that was removed.</param>
[PolymorphicSerialization]
public partial record DocumentTypeTagRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string Key,
    [property: DataMember(Order = 2)]
    string Value)
    : DocumentTypeEvent(Id)
{
}