namespace Hexalith.Documents.Events.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event related to a document storage.
/// </summary>
/// <param name="Id">The unique identifier of the document storage.</param>
[PolymorphicSerialization]
public abstract partial record DocumentStorageEvent([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate identifier.
    /// </summary>
    /// <value>The aggregate identifier.</value>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the name of the aggregate.
    /// </summary>
    /// <value>The name of the aggregate.</value>
    public static string AggregateName => DocumentDomainHelper.DocumentStorageAggregateName;
} 