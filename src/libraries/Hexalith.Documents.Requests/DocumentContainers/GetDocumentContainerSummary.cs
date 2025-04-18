namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get a document container summary.
/// </summary>
/// <param name="Id">The identifier of the document container.</param>
/// <param name="Result">The document container summary view model.</param>
[PolymorphicSerialization]
public partial record GetDocumentContainerSummary(
    string Id,
    [property: DataMember(Order = 3)] DocumentContainerSummaryViewModel? Result = null)
    : DocumentContainerRequest(Id);