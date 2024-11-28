namespace Hexalith.Documents.Events.Documents;

using System.Runtime.Serialization;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a document is created.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Name">The name of the document.</param>
/// <param name="Description">The description of the document.</param>
/// <param name="File">The file description containing metadata about the document file.</param>
/// <param name="OwnerId">The identifier of the document owner.</param>
/// <param name="CreatedOn">The date and time when the document was created.</param>
/// <param name="DocumentTypeId">The identifier of the document type.</param>
[PolymorphicSerialization]
public partial record DocumentAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Description,
    [property: DataMember(Order = 4)]
    FileDescription File,
    [property: DataMember(Order = 5)]
    string OwnerId,
    [property: DataMember(Order = 6)]
    DateTimeOffset CreatedOn,
    [property: DataMember(Order = 7)]
    string DocumentTypeId) : DocumentEvent(Id)
{
}