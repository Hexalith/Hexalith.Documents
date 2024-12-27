namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public partial record GetDocumentContainerSummary(string Id, [property: DataMember(Order = 2)] DocumentContainerSummaryViewModel? Result = null)
    : DocumentContainerRequest(Id);