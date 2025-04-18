namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain.DocumentStorages;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a document partition by its ID.
/// </summary>
/// <param name="Id">The ID of the document partition.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentStorage(string Id, [property: DataMember(Order = 2)] DocumentStorage? Result = null)
    : DocumentStorageRequest(Id);