namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

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