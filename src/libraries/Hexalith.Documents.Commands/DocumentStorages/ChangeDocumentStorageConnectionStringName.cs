namespace Hexalith.Documents.Commands.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record ChangeDocumentStorageConnectionStringName(
    string Id,
    [property: DataMember(Order = 2)] string? ConnectionStringName)
    : DocumentStorageCommand(Id)
{
}