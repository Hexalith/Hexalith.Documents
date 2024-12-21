namespace Hexalith.Documents.Events.DataExports;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

[PolymorphicSerialization]
public abstract partial record DataExportEvent([property: DataMember(Order = 1)] string Id)
{
    public string AggregateId => Id;

    public static string AggregateName => DocumentDomainHelper.DataExportAggregateName;
}