namespace Hexalith.Documents.Commands.Documents;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentActor(string Id, DocumentActor Actor)
    : DocumentCommand(Id)
{
}