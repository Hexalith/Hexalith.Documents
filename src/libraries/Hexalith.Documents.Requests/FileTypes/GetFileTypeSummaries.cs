namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of file type with essential information.
/// </summary>
/// <param name="Skip">The number of file type summaries to skip.</param>
/// <param name="Take">The number of file type summaries to take.</param>
/// <param name="Filter">The search to apply to the file type summaries.</param>
/// <param name="Ids">The list of file type summary IDs.</param>
/// <param name="Results">The list of file type summaries.</param>
[PolymorphicSerialization]
public partial record GetFileTypeSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Search,
    [property: DataMember(Order = 4)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<FileTypeSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummaries"/> class.
    /// </summary>
    public GetFileTypeSummaries()
        : this(0, 0, null, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of file type summaries to skip.</param>
    /// <param name="take">The number of file type summaries to take.</param>
    /// <param name="search">The search to apply to the file type summaries.</param>
    public GetFileTypeSummaries(int skip, int take, string? search = null)
        : this(skip, take, search, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummaries"/> class with specified skip, take, and IDs values.
    /// </summary>
    /// <param name="skip">The number of file type summaries to skip.</param>
    /// <param name="take">The number of file type summaries to take.</param>
    /// <param name="ids">The list of file type summary IDs.</param>
    public GetFileTypeSummaries(IEnumerable<string> ids)
        : this(0, 0, null, ids, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.FileTypeAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.FileTypeAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take, Results = [] };

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results)
        => this with { Results = (IEnumerable<FileTypeSummaryViewModel>)results };
}