namespace Hexalith.Documents.Commands.Documents;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record RemoveDocumentPoint(string Id, string Name)
    : DocumentCommand(Id)
{
}