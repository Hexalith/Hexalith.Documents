namespace Hexalith.Documents.Events.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a cancelled document event.
/// </summary>
/// <param name="Event">The original document event that was cancelled.</param>
/// <param name="Reason">The reason for cancelling the event.</param>
[PolymorphicSerialization]
public partial record DocumentInformationExtractionEventCancelled(
    [property: DataMember(Order = 2)]
    DocumentInformationExtractionEvent Event,
    [property: DataMember(Order = 3)]
    string Reason)
    : DocumentInformationExtractionEvent(Event.Id);