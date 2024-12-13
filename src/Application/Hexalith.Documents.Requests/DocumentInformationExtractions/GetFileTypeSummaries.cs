namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of document information extraction with essential information.
/// </summary>
/// <param name="Skip">The number of document information extraction summaries to skip.</param>
/// <param name="Take">The number of document information extraction summaries to take.</param>
/// <param name="Result">The list of document information extraction summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentInformationExtractionSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentInformationExtractionSummaryViewModel> Result)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionSummaries"/> class.
    /// </summary>
    public GetDocumentInformationExtractionSummaries()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document information xtraction summaries to skip.</param>
    /// <param name="take">The number of document information extraction summaries to take.</param>
    public GetDocumentInformationExtractionSummaries(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentInformationExtractionAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentInformationExtractionAggregateName;
}