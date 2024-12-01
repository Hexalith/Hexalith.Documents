namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to add a new file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Description">The description of the file type.</param>
/// <param name="FileToTextConverter">The file to text converter for the file type.</param>
/// <param name="Targets">The targets for the file type.</param>
[PolymorphicSerialization]
public partial record AddFileType(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Description,
    [property: DataMember(Order = 4)]
    string? FileToTextConverter,
    [property: DataMember(Order = 5)]
    IEnumerable<string> Targets)
    : FileTypeCommand(Id)
{
}