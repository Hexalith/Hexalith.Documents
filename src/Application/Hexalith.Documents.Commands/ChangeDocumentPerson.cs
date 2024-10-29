namespace Hexalith.Documents.Commands;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeDocumentPerson(string Id, Person Person)
    : DocumentCommand(Id)
{
}