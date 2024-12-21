namespace Hexalith.Documents.Requests.Documents;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.ValueObjects;

[DataContract]
public partial record DocumentDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] DocumentDescription Description,
    [property: DataMember(Order = 3)] DocumentRouting? Routing,
    [property: DataMember(Order = 4)] string? ParentDocumentId,
    [property: DataMember(Order = 5)] DocumentState State,
    [property: DataMember(Order = 6)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 7)] FileDescription File,
    [property: DataMember(Order = 8)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 9)] bool Disabled);