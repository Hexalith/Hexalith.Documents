namespace Hexalith.Documents.Events.FileTextExtractionModes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a new file text extraction mode is created.
/// </summary>
/// <param name="Id">The unique identifier for the extraction mode.</param>
/// <param name="Name">The name of the extraction mode.</param>
/// <param name="Description">The optional description of the extraction mode.</param>
/// <param name="FunctionName">The name of the function used for text extraction.</param>
/// <param name="ExtractionInstructions">The instructions used for text extraction.</param>
[PolymorphicSerialization]
public partial record FileTextExtractionModeCreated(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string? Description,
    [property: DataMember(Order = 4)]
    string FunctionName,
    [property: DataMember(Order = 5)]
    string ExtractionInstructions)
    : FileTextExtractionModeEvent(Id)
{
}
