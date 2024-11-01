namespace Hexalith.Documents.Events.FileTextExtractionModes;

using System.Runtime.Serialization;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a cancelled document event.
/// </summary>
/// <param name="Event">The original document event that was cancelled.</param>
/// <param name="Reason">The reason for cancelling the event.</param>
[PolymorphicSerialization]
public partial record FileTextExtractionModeEventCancelled(
    [property: DataMember(Order = 2)]
    FileTextExtractionModeEvent Event,
    [property: DataMember(Order = 3)]
    string Reason)
    : FileTextExtractionModeEvent(Event.Id);
