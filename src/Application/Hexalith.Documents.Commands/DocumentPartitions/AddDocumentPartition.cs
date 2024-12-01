namespace Hexalith.Documents.Commands.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentPartition(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string? Description,
    [property: DataMember(Order = 4)]
    string ConnectionStringName,
    [property: DataMember(Order = 5)]
    IEnumerable<string> Targets)
    : DocumentPartitionCommand(Id)
{
}