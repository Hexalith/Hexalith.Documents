namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document container event is cancelled.
/// </summary>
/// <param name="Event">The original event that was cancelled.</param>
/// <param name="Reason">The reason why the event was cancelled.</param>
[PolymorphicSerialization]
public partial record DocumentContainerEventCancelled(
    DocumentContainerEvent Event,
    [property: DataMember(Order = 2)]
    string Reason)
    : DocumentContainerEvent(Event.Id);