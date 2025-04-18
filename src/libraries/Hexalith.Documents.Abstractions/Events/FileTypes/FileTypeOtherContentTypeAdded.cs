namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when an alternative content type is added to a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="OtherContentType">The additional content type being added to the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeOtherContentTypeAdded(
    string Id,
    [property: DataMember(Order = 2)] string OtherContentType)
    : FileTypeEvent(Id)
{
}