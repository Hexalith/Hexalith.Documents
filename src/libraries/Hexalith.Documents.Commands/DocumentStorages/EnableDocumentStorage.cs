namespace Hexalith.Documents.Commands.DocumentStorages;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record EnableDocumentStorage(string Id) : DocumentStorageCommand(Id)
{
}