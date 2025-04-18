namespace Hexalith.Documents.Commands.DocumentStorages;

using Hexalith.PolymorphicSerializations;

[PolymorphicSerialization]
public partial record DisableDocumentStorage(string Id) : DocumentStorageCommand(Id)
{
}