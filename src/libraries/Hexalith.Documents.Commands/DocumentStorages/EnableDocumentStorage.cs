namespace Hexalith.Documents.Commands.DocumentStorages;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record EnableDocumentStorage(string Id) : DocumentStorageCommand(Id)
{
}