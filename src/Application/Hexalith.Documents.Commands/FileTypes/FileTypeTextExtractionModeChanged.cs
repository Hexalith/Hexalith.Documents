namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record ChangeFileTypeTextExtractionMode(
    string Id,
    [property: DataMember(Order = 2)] string? TextExtractionModeId)
    : FileTypeCommand(Id)
{
}