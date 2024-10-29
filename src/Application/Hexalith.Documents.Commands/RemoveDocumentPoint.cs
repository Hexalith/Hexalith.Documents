namespace Hexalith.Documents.Commands;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record RemoveDocumentPoint(string Id, string Name)
    : DocumentCommand(Id)
{
}