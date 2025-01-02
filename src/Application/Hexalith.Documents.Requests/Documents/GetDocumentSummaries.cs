namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of document with essential information.
/// </summary>
/// <param name="Skip">The number of document summaries to skip.</param>
/// <param name="Take">The number of document summaries to take.</param>
/// <param name="Results">The list of document summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Filter,
    [property: DataMember(Order = 4)] IEnumerable<DocumentSummaryViewModel> Results) : IFilteredChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentSummaries"/> class.
    /// </summary>
    public GetDocumentSummaries()
        : this(0, 0, null, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document summaries to skip.</param>
    /// <param name="take">The number of document summaries to take.</param>
    /// <param name="filter">The filter criteria for the document summaries.</param>
    public GetDocumentSummaries(int skip, int take, string? filter)
        : this(skip, take, filter, [])
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

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results)
        => this with { Results = (IEnumerable<DocumentSummaryViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take, Results = [] };
}