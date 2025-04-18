namespace Hexalith.Documents.Events.Documents;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a document tag removed event.
/// </summary>
/// <param name="Id">The document ID.</param>
/// <param name="Key">The tag key.</param>
[PolymorphicSerialization]
public partial record DocumentTagRemoved(string Id, [property: DataMember(Order = 2)] string Key)
    : DocumentEvent(Id)
{
}