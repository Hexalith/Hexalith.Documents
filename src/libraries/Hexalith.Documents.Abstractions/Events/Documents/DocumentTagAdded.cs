namespace Hexalith.Documents.Events.Documents;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Event raised when a tag is added to a document.
/// </summary>
/// <param name="Id">The document identifier.</param>
/// <param name="Key">The tag identifier.</param>
/// <param name="Value">The tag value.</param>
[PolymorphicSerialization]
public partial record DocumentTagAdded(
    string Id,
    [property: DataMember(Order = 2)] string Key,
    [property: DataMember(Order = 3)] string Value,
    [property: DataMember(Order = 4)] bool Unique)
    : DocumentEvent(Id)
{
}