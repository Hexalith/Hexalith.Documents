namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record FileTypeFileExtensionChanged(
    string Id,
    [property: DataMember(Order = 3)] string FileExtension)
    : FileTypeEvent(Id)
{
}