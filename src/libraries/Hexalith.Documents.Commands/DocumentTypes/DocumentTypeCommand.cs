namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents the base command for all document type operations.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <remarks>
/// This is the base record for all document type commands in the system.
/// It provides common functionality and properties used across all document type operations.
/// </remarks>
[PolymorphicSerialization]
public abstract partial record DocumentTypeCommand([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentTypeAggregateName;
}