namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;

[DataContract]
public partial record DocumentContainerDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Description,
    [property: DataMember(Order = 4)] string? AutomaticRoutingInstructions,
    [property: DataMember(Order = 5)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 6)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 7)] IImmutableDictionary<string, string> Tags,
    [property: DataMember(Order = 8)] bool Disabled)
{
}