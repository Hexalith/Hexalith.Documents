namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record DocumentActorRemoved(string Id, string ContactId)
    : DocumentEvent(Id)
{
}