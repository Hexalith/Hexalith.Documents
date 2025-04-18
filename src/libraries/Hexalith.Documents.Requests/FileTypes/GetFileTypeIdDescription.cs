namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get the description of a file type by its ID.
/// </summary>
/// <param name="Id">The ID of the file type.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetFileTypeIdDescription(string Id, [property: DataMember(Order = 2)] IdDescription? Result = null)
    : FileTypeRequest(Id);