namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a file type's text extraction mode is changed.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="TextExtractionModeId">The identifier of the new text extraction mode, or null if text extraction is disabled.</param>
[PolymorphicSerialization]
public partial record FileTypeTextExtractionModeChanged(
    string Id, 
    [property: DataMember(Order = 2)] string? TextExtractionModeId) 
    : FileTypeEvent(Id)
{
}
