namespace Hexalith.Documents.Requests.FileTypes;

using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Represents the details of a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Description">The description of the file type.</param>
/// <param name="FileToTextConverter">The file type file to text converter.</param>
/// <param name="Targets">The targets associated with the file type.</param>
/// <param name="Disabled">Indicates whether the file type is disabled.</param>
[DataContract]
public partial record FileTypeDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Description,
    [property: DataMember(Order = 4)] string? FileToTextConverter,
    [property: DataMember(Order = 5)] IEnumerable<string> Targets,
    [property: DataMember(Order = 6)] bool Disabled)
{
}