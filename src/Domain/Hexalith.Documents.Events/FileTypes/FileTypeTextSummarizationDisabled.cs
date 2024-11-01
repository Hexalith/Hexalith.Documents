namespace Hexalith.Documents.Events.FileTypes;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when text summarization is disabled for a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeTextSummarizationDisabled(string Id) : FileTypeEvent(Id)
{
}
