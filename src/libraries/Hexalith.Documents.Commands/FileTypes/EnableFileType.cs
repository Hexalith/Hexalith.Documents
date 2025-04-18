namespace Hexalith.Documents.Commands.FileTypes;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record EnableFileType(string Id) : FileTypeCommand(Id)
{
}