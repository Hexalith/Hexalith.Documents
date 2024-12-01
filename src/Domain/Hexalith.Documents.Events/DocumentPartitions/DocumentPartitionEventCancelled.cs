namespace Hexalith.Documents.Events.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentPartitionEventCancelled(
    [property: DataMember(Order = 2)] DocumentPartitionEvent Event,
    [property: DataMember(Order = 3)] string Reason)
    : DocumentPartitionEvent(Event.Id)
{
}