namespace Hexalith.Documents.Events.FileTypes;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a file type is enabled.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeEnabled(string Id) : FileTypeEvent(Id)
{
}