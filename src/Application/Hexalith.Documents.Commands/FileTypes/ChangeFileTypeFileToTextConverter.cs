namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeFileTypeFileToTextConverter(
    string Id,
    [property: DataMember(Order = 2)] string? FileToTextConverter)
    : FileTypeCommand(Id)
{
}