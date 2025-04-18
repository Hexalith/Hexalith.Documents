namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of document type with essential information.
/// </summary>
/// <param name="Skip">The number of document type summaries to skip.</param>
/// <param name="Take">The number of document type summaries to take.</param>
/// <param name="Filter">The search to apply to the document type summaries.</param>
/// <param name="Ids">The list of document type summary IDs.</param>
/// <param name="Results">The list of document type summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentTypeSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Search,
    [property: DataMember(Order = 4)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<DocumentTypeSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeSummaries"/> class.
    /// </summary>
    public GetDocumentTypeSummaries()
        : this(0, 0, null, [], Array.Empty<DocumentTypeSummaryViewModel>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document type summaries to skip.</param>
    /// <param name="take">The number of document type summaries to take.</param>
    /// <param name="search">The search to apply to the document type summaries.</param>
    public GetDocumentTypeSummaries(int skip, int take, string? search = null)
        : this(skip, take, search, [], Array.Empty<DocumentTypeSummaryViewModel>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeSummaries"/> class with specified skip, take, and IDs values.
    /// </summary>
    /// <param name="skip">The number of document type summaries to skip.</param>
    /// <param name="take">The number of document type summaries to take.</param>
    /// <param name="ids">The list of document type summary IDs.</param>
    public GetDocumentTypeSummaries(IEnumerable<string> ids)
        : this(0, 0, null, ids, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentTypeAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentTypeAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results)
        => this with { Results = (IEnumerable<DocumentTypeSummaryViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take, Results = [] };
}