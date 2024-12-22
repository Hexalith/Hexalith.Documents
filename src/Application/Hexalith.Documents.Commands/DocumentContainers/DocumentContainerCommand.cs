namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents the base class for all document container commands.
/// </summary>
/// <param name="Id">The identifier of the document container.</param>
[PolymorphicSerialization]
public abstract partial record DocumentContainerCommand(
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