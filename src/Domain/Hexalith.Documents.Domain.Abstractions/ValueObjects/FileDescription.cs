namespace Hexalith.Document.Domain.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents a description of a file in the system.
/// </summary>
[DataContract]
public record FileDescription(
    /// <summary>
    /// Gets the unique identifier of the file.
    /// </summary>
    [property:DataMember(Order = 1)]
    string Id,

    /// <summary>
    /// Gets the name of the file in the system.
    /// </summary>
    [property : DataMember(Order = 2)]
    string Name,

    /// <summary>
    /// Gets the original name of the file before it was uploaded.
    /// </summary>
    [property : DataMember(Order = 3)]
    string OriginalName,

    /// <summary>
    /// Gets the MIME content type of the file.
    /// </summary>
    [property:DataMember(Order = 4)]
    string ContentType);