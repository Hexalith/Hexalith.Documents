namespace Hexalith.Documents.Events;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DocumentPointRemoved(string Id, string Name)
    : DocumentEvent(Id)
{
}