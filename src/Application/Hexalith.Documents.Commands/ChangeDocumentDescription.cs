namespace Hexalith.Documents.Commands;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a change of the name of a document.
/// </summary>
/// <param name="Id">The document identifier.</param>
/// <param name="Name">The document name.</param>
/// <param name="Description">The document description.</param>
[PolymorphicSerialization]
public partial record ChangeDocumentDescription(string Id, string Name, string Description) : DocumentCommand(Id)
{
}