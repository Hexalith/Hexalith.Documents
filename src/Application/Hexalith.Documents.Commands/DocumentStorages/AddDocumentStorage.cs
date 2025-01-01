namespace Hexalith.Documents.Commands.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentStorage(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    DocumentStorageType StorageType,
    [property: DataMember(Order = 4)]
    string? Comments,
    [property: DataMember(Order = 5)]
    string? ConnectionString)
    : DocumentStorageCommand(Id)
{
}