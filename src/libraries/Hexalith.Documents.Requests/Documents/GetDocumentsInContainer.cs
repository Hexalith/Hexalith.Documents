namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get documents in a specific container with pagination support.
/// </summary>
/// <param name="Skip">The number of documents to skip.</param>
/// <param name="Take">The number of documents to take.</param>
/// <param name="DocumentContainerId">The ID of the document container.</param>
/// <param name="Results">The results of the document query.</param>
[PolymorphicSerialization]
public partial record GetDocumentsInContainer(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 4)] string DocumentContainerId,
    [property: DataMember(Order = 5)] IEnumerable<DocumentSummaryViewModel> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentsInContainer"/> class.
    /// </summary>
    public GetDocumentsInContainer()
        : this(0, 0, string.Empty, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentsInContainer"/> class with specified parameters.
    /// </summary>
    /// <param name="skip">The number of documents to skip.</param>
    /// <param name="take">The number of documents to take.</param>
    /// <param name="documentContainerId">The ID of the document container.</param>
    public GetDocumentsInContainer(int skip, int take, string documentContainerId)
        : this(skip, take, documentContainerId, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentsInContainer"/> class with a specified document container ID.
    /// </summary>
    /// <param name="documentContainerId">The ID of the document container.</param>
    public GetDocumentsInContainer(string documentContainerId)
        : this(0, 0, documentContainerId, [])
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