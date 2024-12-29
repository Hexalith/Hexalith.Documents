namespace Hexalith.Documents.Events.DocumentStorages;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentStorageDisabled(string Id) : DocumentStorageEvent(Id)
{
}