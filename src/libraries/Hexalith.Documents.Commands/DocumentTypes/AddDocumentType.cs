namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to create a new document type in the system.
/// </summary>
/// <param name="Id">The unique identifier for the document type.</param>
/// <param name="Name">The display name of the document type.</param>
/// <param name="Description">Optional detailed description of the document type's purpose.</param>
/// <param name="FileTypeIds">Collection of file type identifiers supported by this document type.</param>
/// <remarks>
/// This command is used to initialize a new document type with its basic properties and supported file types.
/// The document type will be created in an enabled state.
/// </remarks>
[PolymorphicSerialization]
public partial record AddDocumentType(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string? Description,
    [property: DataMember(Order = 4)] IEnumerable<string> DataExtractionIds,
    [property: DataMember(Order = 4)]
    IEnumerable<string> FileTypeIds)
    : DocumentTypeCommand(Id)
{
}