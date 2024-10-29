namespace Hexalith.Documents.Events;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentPointAdded(string Id, DocumentPoint DocumentPoint)
    : DocumentEvent(Id)
{
}