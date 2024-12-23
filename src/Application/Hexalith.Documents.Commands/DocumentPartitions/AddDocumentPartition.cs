namespace Hexalith.Documents.Commands.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentPartition(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    DocumentStorageType StorageType,
    [property: DataMember(Order = 4)]
    string? Description,
    [property: DataMember(Order = 5)]
    string ConnectionString)
    : DocumentPartitionCommand(Id)
{
}