namespace Hexalith.Documents.Commands.FileTypes;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an command that is raised when text summarization is disabled for a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
[PolymorphicSerialization]
public partial record DisableFileTypeTextSummarization(string Id) : FileTypeCommand(Id)
{
}