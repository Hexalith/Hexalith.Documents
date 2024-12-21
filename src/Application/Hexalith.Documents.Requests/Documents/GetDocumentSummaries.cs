namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of document with essential information.
/// </summary>
/// <param name="Skip">The number of document summaries to skip.</param>
/// <param name="Take">The number of document summaries to take.</param>
/// <param name="Result">The list of document summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentSummaryViewModel> Result)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentSummaries"/> class.
    /// </summary>
    public GetDocumentSummaries()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document summaries to skip.</param>
    /// <param name="take">The number of document summaries to take.</param>
    public GetDocumentSummaries(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentAggregateName;
}