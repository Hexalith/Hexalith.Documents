namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document storage event is cancelled.
/// </summary>
/// <param name="Event">The document storage event that was cancelled.</param>
/// <param name="Reason">The reason for cancelling the event.</param>
[PolymorphicSerialization]
public partial record DocumentStorageEventCancelled(
    [property: DataMember(Order = 2)] DocumentStorageEvent Event,
    [property: DataMember(Order = 3)] string Reason)
    : DocumentStorageEvent(Event.Id)
{
} 