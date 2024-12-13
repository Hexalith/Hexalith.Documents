namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of document container with essential information.
/// </summary>
/// <param name="Skip">The number of document container summaries to skip.</param>
/// <param name="Take">The number of document container summaries to take.</param>
/// <param name="Result">The list of document container summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentContainerSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentContainerSummaryViewModel> Result)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerSummaries"/> class.
    /// </summary>
    public GetDocumentContainerSummaries()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document container summaries to skip.</param>
    /// <param name="take">The number of document container summaries to take.</param>
    public GetDocumentContainerSummaries(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentContainerAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentContainerAggregateName;
}