namespace Hexalith.Documents.Commands;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record AddDocumentPoint(string Id, DocumentPoint DocumentPoint)
    : DocumentCommand(Id)
{
}