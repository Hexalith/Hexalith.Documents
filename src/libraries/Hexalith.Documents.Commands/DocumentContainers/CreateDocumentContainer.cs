namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an command that occurs when a new document container is created.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="DocumentStorageId">The unique identifier of the document partition.</param>
/// <param name="Path">The path of the document container.</param>
/// <param name="Name">The name of the document container.</param>
/// <param name="Comments">The description of the document container.</param>
/// <param name="AutomaticRoutingInstructions">The automatic routing instructions for the document container.</param>
[PolymorphicSerialization]
public partial record CreateDocumentContainer(
    string Id,
    [property: DataMember(Order = 2)] string DocumentStorageId,
    [property: DataMember(Order = 3)] string Name,
    [property: DataMember(Order = 3)] string Path,
    [property: DataMember(Order = 4)] string? Comments,
    [property: DataMember(Order = 5)] string? AutomaticRoutingInstructions)
    : DocumentContainerCommand(Id)
{
}