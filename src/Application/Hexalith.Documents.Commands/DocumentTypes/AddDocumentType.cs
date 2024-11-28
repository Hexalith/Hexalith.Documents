namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to create a new document type in the system.
/// </summary>
/// <param name="Id">Unique identifier for the document type.</param>
/// <param name="Name">Display name of the document type.</param>
/// <param name="Description">Detailed description of the document type's purpose.</param>
[PolymorphicSerialization]
public partial record AddDocumentType(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string? Description,
    [property: DataMember(Order = 4)]
    IEnumerable<string> FileTypeIds)
    : DocumentTypeCommand(Id)
{
}