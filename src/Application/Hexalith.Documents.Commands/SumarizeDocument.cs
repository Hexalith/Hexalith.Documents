namespace Hexalith.Documents.Commands;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a document enabled event.
/// </summary>
[PolymorphicSerialization]
public partial record SumarizeDocument(string Id, string Summary) : DocumentCommand(Id)
{
}