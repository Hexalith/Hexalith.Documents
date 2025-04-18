namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a new file type is added.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="ContentType">The primary content type of the file type.</param>
/// <param name="OtherContentTypes">A collection of other content types associated with the file type.</param>
/// <param name="Description">An optional description of the file type.</param>
/// <param name="FileToTextConverter">An optional converter for converting files of this type to text.</param>
[PolymorphicSerialization]
public partial record FileTypeAdded(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string ContentType,
    [property: DataMember(Order = 4)] IEnumerable<string> OtherContentTypes,
    [property: DataMember(Order = 5)] string FileExtension,
    [property: DataMember(Order = 7)] string? Description,
    [property: DataMember(Order = 8)] string? FileToTextConverter)
    : FileTypeEvent(Id)
{
}