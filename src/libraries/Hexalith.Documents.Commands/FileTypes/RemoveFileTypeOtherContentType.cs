namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record RemoveFileTypeOtherContentType(
    string Id,
    [property: DataMember(Order = 2)] string OtherContentType)
    : FileTypeCommand(Id)
{
}