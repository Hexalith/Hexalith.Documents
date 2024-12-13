namespace Hexalith.Documents.Requests.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;
using Hexalith.UI.Components.ViewModels;

/// <summary>
/// Represents a request to get the description of a document partition by its ID.
/// </summary>
/// <param name="Id">The ID of the document partition.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentPartitionIdDescription(string Id, [property: DataMember(Order = 2)] IdDescription? Result = null)
    : DocumentPartitionRequest(Id);