namespace Hexalith.Documents.Commands.DocumentTypes;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a document type is enabled in the system.
/// </summary>
/// <param name="Id">The unique identifier of the document type that was enabled.</param>
[PolymorphicSerialization]
public partial record EnableDocumentType(string Id) : DocumentTypeCommand(Id)
{
}