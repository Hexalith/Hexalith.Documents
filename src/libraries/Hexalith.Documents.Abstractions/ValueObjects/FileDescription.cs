namespace Hexalith.Documents.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents a description of a file in the system.
/// </summary>
/// <param name="Id">The unique identifier of the file.</param>
/// <param name="FileTypeId">The type of the file.</param>
/// <param name="Name">The name of the file.</param>
/// <param name="OriginalName">The original name of the file.</param>
/// <param name="Size">The size of the file.</param>
/// <param name="ContentType">The content type of the file.</param>
[DataContract]
public record FileDescription
(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string? FileTypeId,
    [property: DataMember(Order = 3)] string Name,
    [property: DataMember(Order = 4)] string OriginalName,
    [property: DataMember(Order = 5)] long Size,
    [property: DataMember(Order = 6)] string ContentType);