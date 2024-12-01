namespace Hexalith.Documents.Events.DocumentPartitions;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentPartitionEnabled(string Id) : DocumentPartitionEvent(Id)
{
}