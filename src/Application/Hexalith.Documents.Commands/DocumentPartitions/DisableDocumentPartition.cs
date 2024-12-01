namespace Hexalith.Documents.Commands.DocumentPartitions;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DisableDocumentPartition(string Id) : DocumentPartitionCommand(Id)
{
}