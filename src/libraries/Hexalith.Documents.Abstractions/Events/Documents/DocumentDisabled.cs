namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a document disabled event.
/// </summary>
[PolymorphicSerialization]
public partial record DocumentDisabled(string Id) : DocumentEvent(Id)
{
}