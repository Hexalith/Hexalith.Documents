namespace Hexalith.Documents.Commands.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeDocumentPartitionDescription(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Description)
    : DocumentPartitionCommand(Id)
{
}