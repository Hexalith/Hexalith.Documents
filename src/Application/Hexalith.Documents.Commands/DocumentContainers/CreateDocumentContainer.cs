namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an command that occurs when a new document container is created.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="DocumentPartitionId">The unique identifier of the document partition.</param>
/// <param name="Name">The name of the document container.</param>
/// <param name="Description">The description of the document container.</param>
/// <param name="FileTypeIds">The collection of file type identifiers supported by this container.</param>
[PolymorphicSerialization]
public partial record CreateDocumentContainer(
    string Id,
    [property: DataMember(Order = 2)]
    string DocumentPartitionId,
    [property: DataMember(Order = 3)]
    string Name,
    [property: DataMember(Order = 4)]
    string Path,
    [property: DataMember(Order = 5)]
    string? Description,
    [property: DataMember(Order = 6)]
    IEnumerable<string> FileTypeIds)
    : DocumentContainerCommand(Id)
{
}