namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a document by its ID.
/// </summary>
/// <param name="Id">The ID of the document.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentDetails(string Id, [property: DataMember(Order = 2)] DocumentDetailsViewModel? Result = null)
    : DocumentRequest(Id), IRequest
{
    /// <summary>
    /// Gets the result of the request.
    /// </summary>
    object? IRequest.Result => Result;
}