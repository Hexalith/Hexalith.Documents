namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record ChangeFileTypeFileExtension(
    string Id,
    [property: DataMember(Order = 3)] string FileExtension)
    : FileTypeCommand(Id)
{
}