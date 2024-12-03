namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a tag is removed from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="TagId">The identifier of the tag that was removed.</param>
[PolymorphicSerialization]
public partial record DocumentTypeTagRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string TagId)
    : DocumentTypeEvent(Id)
{
}