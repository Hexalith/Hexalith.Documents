namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that occurs when a tag is removed from a document container.
/// </summary>
[PolymorphicSerialization]
public partial record DocumentContainerTagRemoved(
    /// <param name="Id">The unique identifier of the document container.</param>
    string Id,
    /// <param name="TagId">The unique identifier of the tag that was removed.</param>
    [property: DataMember(Order = 2)]
    string TagId)
    : DocumentContainerEvent(Id)
{
}