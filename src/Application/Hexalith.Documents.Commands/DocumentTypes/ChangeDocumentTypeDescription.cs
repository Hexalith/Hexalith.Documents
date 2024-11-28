namespace Hexalith.Documents.Commands.DocumentTypes;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to update the name and description of an existing document type.
/// </summary>
/// <param name="Id">Identifier of the document type to update.</param>
/// <param name="Name">New name for the document type.</param>
/// <param name="Description">New description for the document type.</param>
[PolymorphicSerialization]
public partial record ChangeDocumentTypeDescription(string Id, string Name, string Description) : DocumentTypeCommand(Id)
{
}