namespace Hexalith.Documents.Events;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentPersonChanged(string Id, Person Person)
    : DocumentEvent(Id)
{
}