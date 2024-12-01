namespace Hexalith.Documents.Events.DocumentPartitions;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentPartitionDisabled(string Id) : DocumentPartitionEvent(Id)
{
}