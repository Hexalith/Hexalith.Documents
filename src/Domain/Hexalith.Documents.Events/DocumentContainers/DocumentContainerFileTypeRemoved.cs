namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that occurs when a file type is removed from a document container's supported file types.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="FileTypeId">The identifier of the file type being removed.</param>
[PolymorphicSerialization]
public partial record DocumentContainerFileTypeRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string FileTypeId)
    : DocumentContainerEvent(Id)
{
}