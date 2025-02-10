namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get the description of a document container by its ID.
/// </summary>
/// <param name="Id">The ID of the document container.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDocumentContainerDetails(string Id, [property: DataMember(Order = 2)] DocumentContainerDetailsViewModel? Result = null)
    : DocumentContainerRequest(Id), IRequest
{
    /// <inheritdoc/>
    object? IRequest.Result => Result;
}