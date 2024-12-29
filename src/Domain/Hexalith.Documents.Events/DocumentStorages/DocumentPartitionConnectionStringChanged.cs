namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentStorageConnectionStringChanged(
    string Id,
    [property: DataMember(Order = 2)] string ConnectionString)
    : DocumentStorageEvent(Id)
{
}