namespace Hexalith.Documents.Commands.DocumentTypes;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to deactivate a document type in the system.
/// </summary>
/// <param name="Id">Identifier of the document type to disable.</param>
[PolymorphicSerialization]
public partial record DisableDocumentType(string Id) : DocumentTypeCommand(Id)
{
}