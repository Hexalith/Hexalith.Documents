namespace Hexalith.Documents.Events.DataManagements;

using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents the base class for all data management events.
/// </summary>
/// <param name="Id">The identifier of the data management operation.</param>
[PolymorphicSerialization]
public abstract partial record DataManagementEvent([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate identifier for the data management.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the name of the aggregate type for data management.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DataManagementAggregateName;
}