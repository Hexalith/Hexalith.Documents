namespace Hexalith.Documents.Domain.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents an actor (user or system) that has a role or permission level in relation to a document.
/// </summary>
/// <param name="ContactId">The contact identifier used as an id of the actor.</param>
/// <param name="Role">The role or permission level of the actor.</param>
[DataContract]
public record DocumentActor([property: DataMember(Order = 1)] string ContactId, [property: DataMember(Order = 2)] DocumentActorRole Role)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentActor"/> class.
    /// </summary>
    public DocumentActor()
        : this(string.Empty, DocumentActorRole.Reader)
    {
    }
}