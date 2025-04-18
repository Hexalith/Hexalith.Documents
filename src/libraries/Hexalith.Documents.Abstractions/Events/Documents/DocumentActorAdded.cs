namespace Hexalith.Documents.Events.Documents;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record DocumentActorAdded(string Id, DocumentActor Actor)
    : DocumentEvent(Id)
{
}