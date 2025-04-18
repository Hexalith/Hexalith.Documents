namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when the automatic routing instructions of a document container are changed.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="AutomaticRoutingInstructions">The new routing instructions, or null if instructions are being cleared.</param>
[PolymorphicSerialization]
public partial record DocumentContainerAutomaticRoutingInstructionsChanged(
    string Id,
    [property: DataMember(Order = 2)]
    string? AutomaticRoutingInstructions)
    : DocumentContainerEvent(Id)
{
}