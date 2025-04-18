namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a tag is added to a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Key">The identifier of the tag being added.</param>
/// <param name="Value">The value associated with the tag.</param>
[PolymorphicSerialization]
public partial record DocumentTypeTagAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string Key,
    [property: DataMember(Order = 3)]
    string Value,
    [property: DataMember(Order = 3)]
    bool Unique)
    : DocumentTypeEvent(Id)
{
}