namespace Hexalith.Documents.Commands.FileTypes;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DisableFileType(string Id) : FileTypeCommand(Id)
{
}