namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a file type by its ID.
/// </summary>
/// <param name="Id">The ID of the file type.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetFileTypeDetails(string Id, [property: DataMember(Order = 2)] FileTypeDetailsViewModel? Result = null)
    : FileTypeRequest(Id), IRequest
{
    /// <inheritdoc/>
    object? IRequest.Result => Result;
}