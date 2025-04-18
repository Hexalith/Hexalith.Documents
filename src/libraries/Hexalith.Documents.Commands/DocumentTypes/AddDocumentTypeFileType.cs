namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to add a supported file type to an existing document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="FileTypeId">The identifier of the file type to be added as supported.</param>
/// <remarks>
/// This command extends the document type's capabilities by adding support for an additional file type.
/// Documents of the specified file type can then be processed under this document type.
/// </remarks>
[PolymorphicSerialization]
public partial record AddDocumentTypeFileType(
    string Id,
    [property: DataMember(Order = 2)]
    string FileTypeId)
    : DocumentTypeCommand(Id)
{
}