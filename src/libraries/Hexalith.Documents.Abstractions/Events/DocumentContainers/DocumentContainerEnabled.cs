namespace Hexalith.Documents.Events.DocumentContainers;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document container is enabled.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
[PolymorphicSerialization]
public partial record DocumentContainerEnabled(string Id) : DocumentContainerEvent(Id)
{
}