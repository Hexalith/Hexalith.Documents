namespace Hexalith.Documents.Events;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a document disabled event.
/// </summary>
[PolymorphicSerialization]
public partial record DocumentDisabled(string Id) : DocumentEvent(Id)
{
}