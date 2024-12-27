namespace Hexalith.Documents.Events.DocumentStorages;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentStorageEnabled(string Id) : DocumentStorageEvent(Id)
{
}