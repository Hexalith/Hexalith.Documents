namespace Hexalith.Documents.Commands.DocumentTypes;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to enable a previously disabled document type in the system.
/// </summary>
/// <param name="Id">The unique identifier of the document type to enable.</param>
/// <remarks>
/// When a document type is enabled, it becomes available for use with new documents.
/// This command restores full functionality to a previously disabled document type.
/// </remarks>
[PolymorphicSerialization]
public partial record EnableDocumentType(string Id) : DocumentTypeCommand(Id)
{
}