namespace Hexalith.Documents.Commands.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeDocumentStorageDescription(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments)
    : DocumentStorageCommand(Id)
{
}