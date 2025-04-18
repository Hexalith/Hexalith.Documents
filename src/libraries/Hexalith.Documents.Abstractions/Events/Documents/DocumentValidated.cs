namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a document disabled event.
/// </summary>
[PolymorphicSerialization]
public partial record DocumentValidated(string Id, string ByContactId, DateTimeOffset Date) : DocumentEvent(Id)
{
}