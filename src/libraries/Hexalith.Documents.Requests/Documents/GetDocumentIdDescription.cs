namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Domain.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a document by its ID.
/// </summary>
/// <param name="Id">The ID of the document.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentIdDescription(string Id, [property: DataMember(Order = 2)] IdDescription? Result = null)
    : DocumentRequest(Id);