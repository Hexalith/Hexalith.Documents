namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when an actor is removed from a document container.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="ContactId">The identifier of the actor being removed.</param>
[PolymorphicSerialization]
public partial record DocumentContainerActorRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string ContactId) : DocumentContainerEvent(Id)
{
}