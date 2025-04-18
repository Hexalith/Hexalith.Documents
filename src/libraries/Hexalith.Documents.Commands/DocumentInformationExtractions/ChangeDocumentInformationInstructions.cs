namespace Hexalith.Documents.Commands.DocumentInformationExtractions;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when the extraction instructions of a file text extraction mode are changed.
/// </summary>
/// <param name="Id">The unique identifier of the extraction mode.</param>
/// <param name="Instructions">The new instructions for text extraction.</param>
[PolymorphicSerialization]
public partial record ChangeDocumentInformationInstructions(string Id, string Instructions) : DocumentInformationExtractionCommand(Id)
{
}