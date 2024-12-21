namespace Hexalith.Documents.Requests.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain.DocumentPartitions;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get the description of a document partition by its ID.
/// </summary>
/// <param name="Id">The ID of the document partition.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentPartition(string Id, [property: DataMember(Order = 2)] DocumentPartition? Result = null)
    : DocumentPartitionRequest(Id);