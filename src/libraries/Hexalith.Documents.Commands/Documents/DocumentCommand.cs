namespace Hexalith.Documents.Commands.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a base class for document commands.
/// </summary>
[PolymorphicSerialization]
public abstract partial record DocumentCommand([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentAggregateName;
}