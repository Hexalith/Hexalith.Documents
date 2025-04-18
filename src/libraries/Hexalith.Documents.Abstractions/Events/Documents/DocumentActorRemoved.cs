namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when an actor is removed from a document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="ContactId">The identifier of the actor being removed.</param>
[PolymorphicSerialization]
public partial record DocumentActorRemoved(string Id, string ContactId)
    : DocumentEvent(Id)
{
}