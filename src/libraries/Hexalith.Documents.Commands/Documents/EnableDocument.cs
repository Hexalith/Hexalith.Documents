namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a document enabled event.
/// </summary>
[PolymorphicSerialization]
public partial record EnableDocument(string Id) : DocumentCommand(Id)
{
}