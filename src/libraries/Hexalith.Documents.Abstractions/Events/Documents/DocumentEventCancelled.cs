namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a cancelled document event.
/// </summary>
/// <param name="Event">The original document event that was cancelled.</param>
/// <param name="Reason">The reason for cancelling the event.</param>
[PolymorphicSerialization]
public partial record DocumentEventCancelled(DocumentEvent Event, string Reason)
    : DocumentEvent(Event.Id);