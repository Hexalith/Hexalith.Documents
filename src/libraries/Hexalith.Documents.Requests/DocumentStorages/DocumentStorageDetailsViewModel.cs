namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Domain.ValueObjects;

/// <summary>
/// Represents the details of a document partition.
/// </summary>
/// <param name="Id">The unique identifier of the document partition.</param>
/// <param name="Name">The name of the document partition.</param>
/// <param name="StorageType">The storage type of the document partition.</param>
/// <param name="Comments">The description of the document partition.</param>
/// <param name="ConnectionString">The connection string name associated with the document partition.</param>
/// <param name="Disabled">Indicates whether the document partition is disabled.</param>
[DataContract]
public sealed record DocumentStorageDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] DocumentStorageType StorageType,
    [property: DataMember(Order = 4)] string? Comments,
    [property: DataMember(Order = 5)] string? ConnectionString,
    [property: DataMember(Order = 6)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}