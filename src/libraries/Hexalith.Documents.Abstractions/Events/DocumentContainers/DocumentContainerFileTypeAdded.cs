namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document type is added to a document container's supported document types.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="DocumentTypeId">The identifier of the document type being added.</param>
[PolymorphicSerialization]
public partial record DocumentContainerDocumentTypeAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string DocumentTypeId)
    : DocumentContainerEvent(Id)
{
}