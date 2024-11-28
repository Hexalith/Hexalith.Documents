namespace Hexalith.Documents.Events;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentActorAdded(string Id, DocumentActor Actor)
    : DocumentEvent(Id)
{
}