namespace Hexalith.Documents.Commands.Documents;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentActor(string Id, DocumentActor Actor)
    : DocumentCommand(Id)
{
}