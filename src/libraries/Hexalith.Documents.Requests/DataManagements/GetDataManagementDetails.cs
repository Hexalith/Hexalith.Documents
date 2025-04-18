namespace Hexalith.Documents.Requests.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get the description of a data export by its ID.
/// </summary>
/// <param name="Id">The ID of the data export.</param>
/// <param name="Result">The result containing the ID and description.</param>
[PolymorphicSerialization]
public partial record GetDataManagementDetails(string Id, [property: DataMember(Order = 2)] DataManagementDetailsViewModel? Result = null)
    : DataManagementRequest(Id);