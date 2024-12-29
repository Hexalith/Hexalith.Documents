namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentStorageEventCancelled(
    [property: DataMember(Order = 2)] DocumentStorageEvent Event,
    [property: DataMember(Order = 3)] string Reason)
    : DocumentStorageEvent(Event.Id)
{
}