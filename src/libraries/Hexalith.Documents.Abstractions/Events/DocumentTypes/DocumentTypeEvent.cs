namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a base class for document commands.
/// </summary>
[PolymorphicSerialization]
public abstract partial record DocumentTypeEvent([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate ID.
    /// </summary>
    /// <value>The aggregate ID.</value>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name.
    /// </summary>
    /// <value>The aggregate name.</value>
    public static string AggregateName => DocumentDomainHelper.DocumentTypeAggregateName;
}