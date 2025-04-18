namespace Hexalith.Documents.Events.DocumentStorages;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event indicating that a document storage has been enabled.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
[PolymorphicSerialization]
public partial record DocumentStorageEnabled(string Id) : DocumentStorageEvent(Id)
{
} 