namespace Hexalith.Documents.Commands.DocumentStorages;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record DisableDocumentStorage(string Id) : DocumentStorageCommand(Id)
{
}