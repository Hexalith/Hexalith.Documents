namespace Hexalith.Documents.Requests.DataExports;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get the description of a data export by its ID.
/// </summary>
/// <param name="Id">The ID of the data export.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDataExportDetails(string Id, [property: DataMember(Order = 2)] DataExportDetailsViewModel? Result = null)
    : DataExportRequest(Id);