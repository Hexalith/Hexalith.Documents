namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record FileTypeOtherFileExtensionAdded(
    string Id,
    [property: DataMember(Order = 2)] string OtherFileExtension)
    : FileTypeEvent(Id)
{
}