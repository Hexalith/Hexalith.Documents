namespace Hexalith.Documents.Commands.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public abstract partial record DocumentPartitionCommand([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate identifier.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the name of the aggregate.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentPartitionAggregateName;
}