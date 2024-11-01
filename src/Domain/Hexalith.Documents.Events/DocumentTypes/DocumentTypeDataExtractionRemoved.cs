namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when a data extraction configuration is removed from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="ExtractionId">The identifier of the data extraction configuration that was removed.</param>
[PolymorphicSerialization]
public partial record DocumentTypeDataExtractionRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string ExtractionId) : DocumentTypeEvent(Id)
{
}
