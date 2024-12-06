namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentTagRemoved(string Id, string TagId)
    : DocumentEvent(Id)
{
}