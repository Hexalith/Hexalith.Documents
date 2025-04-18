namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.DocumentInformationExtractions;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of document information extraction with essential information.
/// </summary>
/// <param name="Skip">The number of document information extraction summaries to skip.</param>
/// <param name="Take">The number of document information extraction summaries to take.</param>
/// <param name="Results">The list of document information extraction summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentInformationExtractionExports(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentInformationExtraction> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionExports"/> class.
    /// </summary>
    public GetDocumentInformationExtractionExports()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionExports"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    public GetDocumentInformationExtractionExports(int skip, int take)
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

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<DocumentInformationExtraction>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => new GetDocumentInformationExtractionExports(Skip + Take, Take);
}