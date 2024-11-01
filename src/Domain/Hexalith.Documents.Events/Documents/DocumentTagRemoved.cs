namespace Hexalith.Documents.Events.Document;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentTagRemoved(string Id, string TagId)
    : DocumentEvent(Id)
{
}