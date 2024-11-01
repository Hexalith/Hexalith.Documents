namespace Hexalith.Documents.Events;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a document enabled event.
/// </summary>
[PolymorphicSerialization]
public partial record DocumentSummarized(string Id, string Summary) : DocumentEvent(Id)
{
}