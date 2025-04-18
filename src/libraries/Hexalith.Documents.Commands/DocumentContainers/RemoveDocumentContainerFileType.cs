namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an command that occurs when a file type is removed from a document container's supported file types.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="FileTypeId">The identifier of the file type being removed.</param>
[PolymorphicSerialization]
public partial record RemoveDocumentContainerFileType(
    string Id,
    [property: DataMember(Order = 2)]
    string FileTypeId)
    : DocumentContainerCommand(Id)
{
}