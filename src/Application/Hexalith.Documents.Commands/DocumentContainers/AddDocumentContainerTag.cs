namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an command that occurs when a tag is added to a document container.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="Key">The identifier of the tag being added.</param>
/// <param name="Value">The value associated with the tag.</param>
/// <param name="Unique">A value indicating whether the tag is unique.</param>
[PolymorphicSerialization]
public partial record AddDocumentContainerTag(
    string Id,
    [property: DataMember(Order = 2)]
    string Key,
    [property: DataMember(Order = 3)]
    string? Value,
    [property: DataMember(Order = 4)]
    bool Unique)
    : DocumentContainerCommand(Id)
{
}