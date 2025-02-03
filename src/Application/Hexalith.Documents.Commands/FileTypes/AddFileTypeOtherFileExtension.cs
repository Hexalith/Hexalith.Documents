namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddFileTypeOtherFileExtension(
    string Id,
    [property: DataMember(Order = 2)] string OtherFileExtension)
    : FileTypeCommand(Id)
{
}