namespace Hexalith.Documents.Commands.FileTypes;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record DisableFileType(string Id) : FileTypeCommand(Id)
{
}