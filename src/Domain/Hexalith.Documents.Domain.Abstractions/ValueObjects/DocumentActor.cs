namespace Hexalith.Documents.Domain.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents an actor (user or system) that has specific permissions on a document.
/// </summary>
[DataContract]
public record DocumentActor(
    /// <summary>
    /// Gets the unique identifier of the contact associated with this document actor.
    /// </summary>
    [property: DataMember(Order = 1)] string ContactId,

    /// <summary>
    /// Gets the role or permission level of this actor in relation to the document.
    /// </summary>
    [property: DataMember(Order = 2)] DocumentActorRole Role)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentActor"/> class.
    /// Creates a default document actor with empty contact ID and reader role.
    /// </summary>
    public DocumentActor()
        : this(string.Empty, DocumentActorRole.Reader)
    {
    }
}