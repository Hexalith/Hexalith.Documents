namespace Hexalith.Documents.Commands.DocumentContainers;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an command that occurs when a document container is disabled.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
[PolymorphicSerialization]
public partial record DisableDocumentContainer(string Id) : DocumentContainerCommand(Id)
{
}