namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get the description of a document type by its ID.
/// </summary>
/// <param name="Id">The ID of the document type.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentTypeIdDescription(string Id, [property: DataMember(Order = 2)] IdDescription? Result = null)
    : DocumentTypeRequest(Id);