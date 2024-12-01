namespace Hexalith.Documents.Commands.DocumentPartitions;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record EnableDocumentPartition(string Id) : DocumentPartitionCommand(Id)
{
}