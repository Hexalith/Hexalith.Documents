namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;
using Hexalith.Document.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents the base class for all document container events.
/// </summary>
[PolymorphicSerialization]
public abstract partial record DocumentContainerEvent(
    /// <param name="Id">The unique identifier of the document container.</param>
    [property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate identifier for the document container.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the name of the aggregate type for document containers.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentContainerAggregateName;
}
