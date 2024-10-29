namespace Hexalith.Documents.Events;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentAdded(string Id, string Name, string Description, Person Person)
    : DocumentEvent(Id)
{
}