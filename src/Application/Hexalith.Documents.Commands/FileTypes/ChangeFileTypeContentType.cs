namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeFileTypeContentType(
    string Id,
    [property: DataMember(Order = 3)] string ContentType)
    : FileTypeCommand(Id)
{
}