namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a target is added to a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Target">The identifier of the target being added to the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeTargetAdded(
    string Id,
    [property: DataMember(Order = 2)] string Target)
    : FileTypeEvent(Id)
{
}