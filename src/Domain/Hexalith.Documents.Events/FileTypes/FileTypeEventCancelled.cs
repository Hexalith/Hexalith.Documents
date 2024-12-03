namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a file type event is cancelled.
/// </summary>
/// <param name="Event">The original file type event that was cancelled.</param>
/// <param name="Reason">The reason why the event was cancelled.</param>
[PolymorphicSerialization]
public partial record FileTypeEventCancelled(
    [property: DataMember(Order = 2)] FileTypeEvent Event,
    [property: DataMember(Order = 3)] string Reason)
    : FileTypeEvent(Event.Id)
{
}