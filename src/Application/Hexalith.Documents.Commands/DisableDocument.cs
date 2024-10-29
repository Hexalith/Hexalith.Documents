namespace Hexalith.Documents.Commands;
/// <summary>
/// Represents a document disabled event.
/// </summary>
public partial record DisableDocument(string Id) : DocumentCommand(Id)
{
}