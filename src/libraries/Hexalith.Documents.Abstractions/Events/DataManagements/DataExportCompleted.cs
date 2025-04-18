namespace Hexalith.Documents.Events.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a data export operation is completed.
/// </summary>
/// <param name="Id">The unique identifier of the data management operation.</param>
/// <param name="Size">The size of the exported data in bytes.</param>
/// <param name="DateTime">The date and time when the export operation completed.</param>
[PolymorphicSerialization]
public partial record DataExportCompleted(
    string Id,
    [property: DataMember(Order = 2)] long Size,
    [property: DataMember(Order = 3)] DateTimeOffset DateTime)
    : DataManagementEvent(Id)
{
}