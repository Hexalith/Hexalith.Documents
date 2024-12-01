namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to add a new file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Description">The description of the file type.</param>
/// <param name="TextExtractionModeId">The text extraction mode identifier.</param>
/// <param name="SummarizationEnabled">Indicates if summarization is enabled.</param>
/// <param name="SummarizationInstructions">The instructions for summarization.</param>
/// <param name="Targets">The targets for the file type.</param>
[PolymorphicSerialization]
public partial record AddFileType(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Description,
    [property: DataMember(Order = 4)]
    string? TextExtractionModeId,
    [property: DataMember(Order = 5)]
    bool SummarizationEnabled,
    [property: DataMember(Order = 6)]
    string? SummarizationInstructions,
    [property: DataMember(Order = 7)]
    IEnumerable<string> Targets)
    : FileTypeCommand(Id)
{
}