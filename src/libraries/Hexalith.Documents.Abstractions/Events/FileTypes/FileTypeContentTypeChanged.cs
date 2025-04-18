namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when the content type of a file type is changed.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="ContentType">The new content type for the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeContentTypeChanged(
    string Id,
    [property: DataMember(Order = 3)] string ContentType)
    : FileTypeEvent(Id)
{
}