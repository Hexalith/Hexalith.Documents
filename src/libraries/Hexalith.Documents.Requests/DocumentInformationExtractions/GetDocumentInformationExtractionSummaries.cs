namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of document information extraction with essential information.
/// </summary>
/// <param name="Skip">The number of document information extraction summaries to skip.</param>
/// <param name="Take">The number of document information extraction summaries to take.</param>
/// <param name="Filter">The search to apply to the document information extraction summaries.</param>
/// <param name="Ids">The list of document information extraction summary IDs.</param>
/// <param name="Results">The list of document information extraction summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentInformationExtractionSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Search,
    [property: DataMember(Order = 4)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<DocumentInformationExtractionSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionSummaries"/> class.
    /// </summary>
    public GetDocumentInformationExtractionSummaries()
        : this(0, 0, null, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document information extraction summaries to skip.</param>
    /// <param name="take">The number of document information extraction summaries to take.</param>
    /// <param name="search">The search to apply to the document information extraction summaries.</param>
    public GetDocumentInformationExtractionSummaries(int skip, int take, string? search = null)
        : this(skip, take, search, Array.Empty<string>(), Array.Empty<DocumentInformationExtractionSummaryViewModel>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionSummaries"/> class with specified skip, take, and IDs values.
    /// </summary>
    /// <param name="skip">The number of document information extraction summaries to skip.</param>
    /// <param name="take">The number of document information extraction summaries to take.</param>
    /// <param name="ids">The list of document information extraction summary IDs.</param>
    public GetDocumentInformationExtractionSummaries(IEnumerable<string> ids)
        : this(0, 0, null, ids, Array.Empty<DocumentInformationExtractionSummaryViewModel>())
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

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results)
        => this with { Results = (IEnumerable<DocumentInformationExtractionSummaryViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take };
}