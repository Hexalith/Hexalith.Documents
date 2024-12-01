namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a new file type is created.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Description">The description of the file type.</param>
/// <param name="TextExtractionModeId">The identifier of the text extraction mode to be used, if any.</param>
/// <param name="SummarizationEnabled">A value indicating whether text summarization is enabled for this file type.</param>
/// <param name="SummarizationInstructions">Optional instructions for text summarization.</param>
/// <param name="Targets">The collection of target identifiers associated with this file type.</param>
[PolymorphicSerialization]
public partial record FileTypeAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string? Description,
    [property: DataMember(Order = 4)]
    string? FileToTextConverter,
    [property: DataMember(Order = 7)]
    IEnumerable<string> Targets)
    : FileTypeEvent(Id)
{
}