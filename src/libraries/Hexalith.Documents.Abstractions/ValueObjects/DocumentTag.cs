namespace Hexalith.Documents.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents a tag associated with a document.
/// </summary>
/// <param name="Key">The key of the tag.</param>
/// <param name="Value">The value of the tag.</param>
/// <param name="Unique">Indicates whether the tag is unique.</param>
[DataContract]
public record DocumentTag
(
    [property: DataMember] string Key,
    [property: DataMember] string? Value,
    [property: DataMember] bool Unique)
{
}