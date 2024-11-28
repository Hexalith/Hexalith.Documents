namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Base class for all document type commands providing common functionality.
/// </summary>
/// <param name="Id">Identifier of the document type being operated on.</param>
[PolymorphicSerialization]
public abstract partial record DocumentTypeCommand([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate identifier for the command.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name for document type commands.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentTypeAggregateName;
}