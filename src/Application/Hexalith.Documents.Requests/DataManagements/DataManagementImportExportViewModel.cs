namespace Hexalith.Documents.Requests.DataManagements;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain.DataManagements;
using Hexalith.Domain.Aggregates;

/// <summary>
/// Represents the details of a data export.
/// </summary>
/// <param name="Id">The unique identifier of the data export.</param>
/// <param name="Size">The size of the data export.</param>
/// <param name="StartedAt">The date and time when the data export started.</param>
/// <param name="CompletedAt">The date and time when the data export completed.</param>
[DataContract]
public sealed record DataManagementImportExportViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 2)] string? Comments,
    [property: DataMember(Order = 3)] DateTimeOffset StartedAt,
    [property: DataMember(Order = 3)] DateTimeOffset? CompletedAt) : IExportModel
{
    /// <inheritdoc/>
    public static IExportModel CreateExportModel(IDomainAggregate aggregate)
    {
        ArgumentNullException.ThrowIfNull(aggregate);
        if (aggregate is DataManagement dataManagement)
        {
            return new DataManagementImportExportViewModel(
                dataManagement.Id,
                dataManagement.Size,
                dataManagement.Comments,
                dataManagement.StartedAt,
                dataManagement.CompletedAt);
        }

        throw new InvalidOperationException($"Invalid aggregate type: {aggregate.GetType().Name}. Expected: {nameof(DataManagement)}.");
    }
}