namespace Hexalith.Documents.Events.DocumentInformationExtractions;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a file text extraction mode is disabled.
/// </summary>
/// <param name="Id">The unique identifier of the extraction mode that was disabled.</param>
[PolymorphicSerialization]
public partial record DocumentInformationExtractionDisabled(string Id) : DocumentInformationExtractionEvent(Id)
{
}