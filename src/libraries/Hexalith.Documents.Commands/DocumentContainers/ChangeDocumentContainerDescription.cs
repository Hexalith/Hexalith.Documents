namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an command that occurs when a document container's description and name are changed.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="Name">The updated name of the document container.</param>
/// <param name="Comments">The updated description of the document container.</param>
[PolymorphicSerialization]
public partial record ChangeDocumentContainerDescription(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string? Comments) : DocumentContainerCommand(Id)
{
}