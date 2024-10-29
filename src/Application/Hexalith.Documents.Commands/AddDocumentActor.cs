namespace Hexalith.Documents.Commands;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentActor(string Id, DocumentActor Actor)
    : DocumentCommand(Id)
{
}