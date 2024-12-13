namespace Hexalith.Documents.Requests.DocumentPartitions;

using System.Runtime.Serialization;

/// <summary>
/// Represents the details of a document partition.
/// </summary>
/// <param name="Id">The unique identifier of the document partition.</param>
/// <param name="Name">The name of the document partition.</param>
/// <param name="Description">The description of the document partition.</param>
/// <param name="ConnectionStringName">The connection string name associated with the document partition.</param>
/// <param name="Disabled">Indicates whether the document partition is disabled.</param>
[DataContract]
public partial record DocumentPartitionDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Description,
    [property: DataMember(Order = 4)] string? ConnectionStringName,
    [property: DataMember(Order = 6)] bool Disabled)
{
}