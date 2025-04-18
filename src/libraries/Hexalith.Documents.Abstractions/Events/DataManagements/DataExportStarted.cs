namespace Hexalith.Documents.Events.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record DataExportStarted(
    string Id,
    [property: DataMember(Order = 2)]
    DateTimeOffset DateTime)
    : DataManagementEvent(Id)
{
}