namespace Hexalith.Documents.Events.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record DocumentAccessKeyAdded(
    string Id,
    [property: DataMember(Order = 2)] DocumentAccessKey AccessKey) : DocumentEvent(Id)
{
}