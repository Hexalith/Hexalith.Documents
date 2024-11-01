namespace Hexalith.Documents.Events.FileTextExtractionModes;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a file text extraction mode is enabled.
/// </summary>
/// <param name="Id">The unique identifier of the extraction mode that was enabled.</param>
[PolymorphicSerialization]
public partial record FileTextExtractionModeEnabled(string Id) : FileTextExtractionModeEvent(Id)
{
}
