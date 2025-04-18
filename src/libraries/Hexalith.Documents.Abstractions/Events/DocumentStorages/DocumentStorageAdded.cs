namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document storage is added.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
/// <param name="Name">The name of the document storage.</param>
/// <param name="Comments">Comments or description of the document storage.</param>
/// <param name="ConnectionString">The connection string for the document storage.</param>
/// <param name="IsDefault">Indicates whether this is the default document storage.</param>
/// <param name="IsDisabled">Indicates whether the document storage is initially disabled.</param>
[PolymorphicSerialization]
public partial record DocumentStorageAdded(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] string ConnectionString,
    [property: DataMember(Order = 5)] bool IsDefault,
    [property: DataMember(Order = 6)] bool IsDisabled) : DocumentStorageEvent(Id)
{
} 