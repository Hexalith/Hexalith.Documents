namespace Hexalith.Documents.Commands;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record RemoveDocumentActor(string Id, string ContactId)
    : DocumentCommand(Id)
{
}