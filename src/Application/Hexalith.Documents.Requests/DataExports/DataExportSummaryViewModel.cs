namespace Hexalith.Documents.Requests.DataExports;

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a summary view model for data export.
/// </summary>
/// <param name="Id">The unique identifier of the data export.</param>
/// <param name="Size">The size of the data export.</param>
/// <param name="StartedAt">The date and time when the data export started.</param>
/// <param name="CompletedAt">The date and time when the data export completed.</param>
[DataContract]
[method: JsonConstructor]
public partial record DataExportSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] DateTimeOffset StartedAt,
    [property: DataMember(Order = 3)] DateTimeOffset? CompletedAt)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataExportSummaryViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the data export.</param>
    /// <exception cref="ArgumentNullException">Thrown when the details parameter is null.</exception>
    public DataExportSummaryViewModel(DataExportDetailsViewModel details)
        : this(
              (details ?? throw new ArgumentNullException(nameof(details))).Id,
              details.Size,
              details.StartedAt,
              details.CompletedAt)
    {
    }
}