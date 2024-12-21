namespace Hexalith.Documents.Events.DataExports;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DataExportCompleted(
    string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] DateTimeOffset DateTime)
    : DataExportEvent(Id)
{
}