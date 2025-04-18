namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when the description of a document storage is changed.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
/// <param name="Name">The name of the document storage.</param>
/// <param name="Comments">The comments or additional description for the document storage.</param>
[PolymorphicSerialization]
public partial record DocumentStorageDescriptionChanged(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments)
    : DocumentStorageEvent(Id)
{
} 