namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event related to a document partition.
/// </summary>
[PolymorphicSerialization]
public abstract partial record DocumentStorageEvent([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate identifier.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the name of the aggregate.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentStorageAggregateName;
}