namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record FileTypeContentTypeChanged(
    string Id,
    [property: DataMember(Order = 3)] string ContentType)
    : FileTypeEvent(Id)
{
}