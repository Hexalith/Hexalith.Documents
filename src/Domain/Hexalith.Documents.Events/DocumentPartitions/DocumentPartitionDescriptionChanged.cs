namespace Hexalith.Documents.Events.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentPartitionDescriptionChanged(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Description)
    : DocumentPartitionEvent(Id)
{
}