namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

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