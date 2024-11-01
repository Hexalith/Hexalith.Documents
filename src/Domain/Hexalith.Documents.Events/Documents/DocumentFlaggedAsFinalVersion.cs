namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a document disabled event.
/// </summary>
[PolymorphicSerialization]
public partial record DocumentFlaggedAsFinalVersion(string Id, string ByContactId, DateTimeOffset Date) : DocumentEvent(Id)
{
}