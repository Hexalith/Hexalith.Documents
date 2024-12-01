namespace Hexalith.Documents.Commands.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a new file text extraction mode is created.
/// </summary>
/// <param name="Id">The unique identifier for the extraction mode.</param>
/// <param name="Name">The name of the extraction mode.</param>
/// <param name="Description">The optional description of the extraction mode.</param>
/// <param name="Instructions">The instructions used for text extraction.</param>
[PolymorphicSerialization]
public partial record AddDocumentInformationExtraction(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Model,
    [property: DataMember(Order = 4)]
    string? Description,
    [property: DataMember(Order = 5)]
    string Instructions)
    : DocumentInformationExtractionCommand(Id)
{
}