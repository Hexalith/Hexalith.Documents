namespace Hexalith.Documents.Events.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record DataExportCompleted(
    string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] DateTimeOffset DateTime)
    : DataManagementEvent(Id)
{
}