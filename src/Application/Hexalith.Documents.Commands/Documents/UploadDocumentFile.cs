namespace Hexalith.Documents.Commands.Documents;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Command to create a new document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="FileTypeId">The type of the document.</param>
/// <param name="Name">The name of the document.</param>
/// <param name="Content">The content of the document in base64.</param>
[PolymorphicSerialization]
public partial record UploadDocumentFile(
    string Id,
    [property: DataMember(Order = 2)] string FileTypeId,
    [property: DataMember(Order = 3)] string Name,
    [property: DataMember(Order = 2)] string ContentType,
    [property: DataMember(Order = 4)] string Content)
    : DocumentCommand(Id)
{
}