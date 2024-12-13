namespace Hexalith.Documents.Requests.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public abstract partial record DocumentPartitionRequest([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentPartitionAggregateName;
}