namespace Hexalith.Documents.Events.Documents;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentTagAdded(string Id, string TagId, string TagValue)
    : DocumentEvent(Id)
{
}