namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an command that occurs when the automatic routing instructions of a document container are changed.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="AutomaticRoutingInstructions">The new routing instructions, or null if instructions are being cleared.</param>
[PolymorphicSerialization]
public partial record ChangeDocumentContainerAutomaticRoutingInstructions(
    string Id,
    [property: DataMember(Order = 2)]
    string? AutomaticRoutingInstructions)
    : DocumentContainerCommand(Id)
{
}