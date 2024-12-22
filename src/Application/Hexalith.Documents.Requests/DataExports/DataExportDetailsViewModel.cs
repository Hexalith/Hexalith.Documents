namespace Hexalith.Documents.Requests.DataExports;

using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Represents the details of a data export.
/// </summary>
/// <param name="Id">The unique identifier of the data export.</param>
/// <param name="Size">The size of the data export.</param>
/// <param name="StartedAt">The date and time when the data export started.</param>
/// <param name="CompletedAt">The date and time when the data export completed.</param>
[DataContract]
public partial record DataExportDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] DateTimeOffset StartedAt,
    [property: DataMember(Order = 3)] DateTimeOffset? CompletedAt)
{
}