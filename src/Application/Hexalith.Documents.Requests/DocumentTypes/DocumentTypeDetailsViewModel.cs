namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.Serialization;

[DataContract]
public partial record DocumentTypeDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Description,
    [property: DataMember(Order = 4)] string? DataInstructions,
    [property: DataMember(Order = 5)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 9)] IImmutableDictionary<string, string> Tags,
    [property: DataMember(Order = 6)] bool Disabled)
{
}