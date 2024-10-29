namespace Hexalith.Documents.Commands;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocument(string Id, string Name, string? Comments, Person Person)
    : DocumentCommand(Id)
{
}