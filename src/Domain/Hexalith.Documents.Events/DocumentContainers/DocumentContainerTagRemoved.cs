namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that occurs when a tag is removed from a document container.
/// </summary>
/// <param name="Id">The identifier of the document container.</param>
/// <param name="TagId">The identifier of the tag that was removed.</param>
[PolymorphicSerialization]
public partial record DocumentContainerTagRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string TagId)
    : DocumentContainerEvent(Id)
{
}