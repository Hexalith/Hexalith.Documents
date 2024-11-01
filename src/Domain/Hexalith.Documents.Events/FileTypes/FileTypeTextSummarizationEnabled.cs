namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when text summarization is enabled for a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="SummarizationInstructions">Optional instructions for how the text should be summarized.</param>
[PolymorphicSerialization]
public partial record FileTypeTextSummarizationEnabled(
    string Id, 
    [property: DataMember(Order = 2)] string? SummarizationInstructions) 
    : FileTypeEvent(Id)
{
}
