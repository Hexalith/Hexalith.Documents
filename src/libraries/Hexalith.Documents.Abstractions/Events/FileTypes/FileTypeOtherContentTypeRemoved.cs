namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record FileTypeOtherContentTypeRemoved(
    string Id,
    [property: DataMember(Order = 2)] string OtherContentType)
    : FileTypeEvent(Id)
{
}