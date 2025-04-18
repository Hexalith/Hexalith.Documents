namespace Hexalith.Documents.Commands.FileTypes;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record EnableFileType(string Id) : FileTypeCommand(Id)
{
}