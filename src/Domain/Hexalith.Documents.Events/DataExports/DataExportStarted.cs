namespace Hexalith.Documents.Events.DataExports;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DataExportStarted(
    string Id,
    [property: DataMember(Order = 2)]
    DateTimeOffset DateTime)
    : DataExportEvent(Id)
{
}