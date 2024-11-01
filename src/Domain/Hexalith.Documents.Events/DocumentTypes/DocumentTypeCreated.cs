namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a new document type is created.
/// </summary>
/// <param name="Id">The unique identifier for the document type.</param>
/// <param name="Name">The name of the document type.</param>
/// <param name="Description">The description of the document type.</param>
/// <param name="FileTypeIds">The collection of file type identifiers associated with this document type.</param>
[PolymorphicSerialization]
public partial record DocumentTypeCreated(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Description,
    [property: DataMember(Order = 4)]
    IEnumerable<string> FileTypeIds)
    : DocumentTypeEvent(Id)
{
}
