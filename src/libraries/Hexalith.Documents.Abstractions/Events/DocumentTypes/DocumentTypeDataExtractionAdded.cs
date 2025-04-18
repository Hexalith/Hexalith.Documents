namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a data extraction configuration is added to a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="ExtractionId">The identifier of the data extraction configuration.</param>
/// <param name="DataInstructions">Optional instructions for the data extraction process. Can be null if no specific instructions are needed.</param>
[PolymorphicSerialization]
public partial record DocumentTypeDataExtractionAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string DataInformationExtractionId) : DocumentTypeEvent(Id)
{
}