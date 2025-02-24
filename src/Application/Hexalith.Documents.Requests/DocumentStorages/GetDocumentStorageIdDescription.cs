namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get the description of a document partition by its ID.
/// </summary>
/// <param name="Id">The ID of the document partition.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentStorageIdDescription(string Id, [property: DataMember(Order = 2)] IdDescription? Result = null)
    : DocumentStorageRequest(Id);