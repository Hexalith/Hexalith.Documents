namespace Hexalith.Documents.Events.DataManagements;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public abstract partial record DataManagementEvent([property: DataMember(Order = 1)] string Id)
{
    public string AggregateId => Id;

    public static string AggregateName => DocumentDomainHelper.DataManagementAggregateName;
}