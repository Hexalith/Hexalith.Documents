namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents a summary view model for document storage.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
/// <param name="Name">The name of the document storage.</param>
/// <param name="Disabled">Indicates whether the document storage is disabled.</param>
[DataContract]
public sealed record DocumentStorageSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}