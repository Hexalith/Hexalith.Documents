namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Documents.Domain.ValueObjects;

[DataContract]
public partial record DocumentTypeExportViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Comments,
    [property: DataMember(Order = 7)] IEnumerable<string> DataExtractionIds,
    [property: DataMember(Order = 8)] IEnumerable<string> FileTypeIds,
    [property: DataMember(Order = 9)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 10)] bool Disabled)
{
}