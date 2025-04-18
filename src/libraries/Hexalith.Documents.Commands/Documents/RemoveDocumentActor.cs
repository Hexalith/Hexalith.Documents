namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record RemoveDocumentActor(string Id, string ContactId)
    : DocumentCommand(Id)
{
}