namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record RemoveDocumentActor(string Id, string ContactId)
    : DocumentCommand(Id)
{
}