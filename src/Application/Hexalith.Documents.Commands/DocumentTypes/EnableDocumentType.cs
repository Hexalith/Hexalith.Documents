namespace Hexalith.Documents.Commands.DocumentTypes;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to activate a document type in the system.
/// </summary>
/// <param name="Id">Identifier of the document type to enable.</param>
[PolymorphicSerialization]
public partial record EnableDocumentType(string Id) : DocumentTypeCommand(Id)
{
}