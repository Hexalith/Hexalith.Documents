namespace Hexalith.Documents.Events;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentActorRemoved(string Id, string ContactId)
    : DocumentEvent(Id)
{
}