namespace Hexalith.Documents.Events.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentPartitionConnectionStringChanged(
    string Id,
    [property: DataMember(Order = 2)] string ConnectionString)
    : DocumentPartitionEvent(Id)
{
}