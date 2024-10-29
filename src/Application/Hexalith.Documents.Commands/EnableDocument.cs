namespace Hexalith.Documents.Commands;

/// <summary>
/// Represents a document enabled event.
/// </summary>
public partial record EnableDocument(string Id) : DocumentCommand(Id)
{
}