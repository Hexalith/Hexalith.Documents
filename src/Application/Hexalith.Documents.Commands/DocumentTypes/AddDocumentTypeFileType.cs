namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a file type is added to a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="FileTypeId">The identifier of the file type that was added to the document type.</param>
[PolymorphicSerialization]
public partial record AddDocumentTypeFileType(
    string Id,
    [property: DataMember(Order = 2)]
    string FileTypeId)
    : DocumentTypeCommand(Id)
{
}