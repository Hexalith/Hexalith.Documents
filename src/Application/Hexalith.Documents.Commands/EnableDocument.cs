namespace Hexalith.Documents.Commands;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a document enabled event.
/// </summary>
[PolymorphicSerialization]
public partial record EnableDocument(string Id) : DocumentCommand(Id)
{
}