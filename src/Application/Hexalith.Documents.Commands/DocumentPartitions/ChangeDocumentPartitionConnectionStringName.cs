namespace Hexalith.Documents.Commands.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeDocumentPartitionConnectionStringName(
    string Id,
    [property: DataMember(Order = 2)] string? ConnectionStringName)
    : DocumentPartitionCommand(Id)
{
}