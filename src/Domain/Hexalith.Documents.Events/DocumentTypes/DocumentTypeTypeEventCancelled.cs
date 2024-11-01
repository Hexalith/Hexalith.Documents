namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a document type event is cancelled.
/// </summary>
/// <param name="Event">The original document type event that was cancelled.</param>
/// <param name="Reason">The reason for cancelling the document type event.</param>
[PolymorphicSerialization]
public partial record DocumentTypeEventCancelled(
    DocumentTypeEvent Event,
    [property: DataMember(Order = 2)]
    string Reason)
    : DocumentTypeEvent(Event.Id);
