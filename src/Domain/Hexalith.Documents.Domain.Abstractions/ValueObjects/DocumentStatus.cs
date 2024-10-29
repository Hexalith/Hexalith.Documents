namespace Hexalith.Document.Domain.ValueObjects;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the current status of a document in its lifecycle.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentStatus
{
    /// <summary>
    /// The document is in draft state and can be modified.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// The document is finalized but not yet validated.
    /// </summary>
    Final = 1,

    /// <summary>
    /// The document has been validated by an authorized person.
    /// </summary>
    Validated = 2,

    /// <summary>
    /// The document has been published and is available for general access.
    /// </summary>
    Published = 3,
}