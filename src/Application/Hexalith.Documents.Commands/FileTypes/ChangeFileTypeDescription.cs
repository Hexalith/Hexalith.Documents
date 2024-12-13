namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeFileTypeDescription(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Description)
    : FileTypeCommand(Id)
{
}