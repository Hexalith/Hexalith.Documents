namespace Hexalith.Documents.Commands.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to create a new document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Name">The name of the document.</param>
/// <param name="Comments">The comments of the document.</param>
/// <param name="Files">The the file description.</param>
/// <param name="OwnerId">The identifier of the document owner.</param>
/// <param name="CreatedOn">The date and time when the document was created.</param>
/// <param name="DocumentTypeId">The identifier of the document type.</param>
[PolymorphicSerialization]
public partial record AddDocument(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 4)] IEnumerable<FileDescription> Files,
    [property: DataMember(Order = 5)] string OwnerId,
    [property: DataMember(Order = 6)] DateTimeOffset CreatedOn,
    [property: DataMember(Order = 7)] string? ParentDocumentId,
    [property: DataMember(Order = 8)] string DocumentContainerId,
    [property: DataMember(Order = 9)] string DocumentTypeId,
    [property: DataMember(Order = 10)] IEnumerable<DocumentTag> Tags)
    : DocumentCommand(Id)
{
}