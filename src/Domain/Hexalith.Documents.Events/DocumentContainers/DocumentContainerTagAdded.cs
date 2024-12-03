namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that occurs when a tag is added to a document container.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="TagId">The identifier of the tag being added.</param>
/// <param name="TagValue">The value associated with the tag.</param>
[PolymorphicSerialization]
public partial record DocumentContainerTagAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string TagId,
    [property: DataMember(Order = 3)]
    string TagValue)
    : DocumentContainerEvent(Id)
{
}