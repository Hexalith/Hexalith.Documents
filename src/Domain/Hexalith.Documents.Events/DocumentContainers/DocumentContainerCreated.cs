namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that occurs when a new document container is created.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="Name">The name of the document container.</param>
/// <param name="Description">The description of the document container.</param>
/// <param name="FileTypeIds">The collection of file type identifiers supported by this container.</param>
[PolymorphicSerialization]
public partial record DocumentContainerCreated(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Description,
    [property: DataMember(Order = 4)]
    IEnumerable<string> FileTypeIds)
    : DocumentContainerEvent(Id)
{
}