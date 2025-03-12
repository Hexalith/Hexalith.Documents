namespace Hexalith.Documents.Requests.FileTypes;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Domain.ValueObjects;

/// <summary>
/// Represents the details of a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="Comments">The description of the file type.</param>
/// <param name="FileToTextConverter">The file type file to text converter.</param>
/// <param name="Targets">The targets associated with the file type.</param>
/// <param name="Disabled">Indicates whether the file type is disabled.</param>
[DataContract]
public sealed record FileTypeDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string ContentType,
    [property: DataMember(Order = 4)] IEnumerable<string> OtherContentTypes,
    [property: DataMember(Order = 5)] string FileExtension,
    [property: DataMember(Order = 7)] string? Comments,
    [property: DataMember(Order = 8)] string? FileToTextConverter,
    [property: DataMember(Order = 9)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}