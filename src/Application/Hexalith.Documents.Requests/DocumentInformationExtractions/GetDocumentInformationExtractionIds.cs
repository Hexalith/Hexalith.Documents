namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get document information extraction IDs with pagination.
/// </summary>
/// <param name="Skip">The number of items to skip.</param>
/// <param name="Take">The number of items to take.</param>
/// <param name="Result">The collection of document information extraction IDs.</param>
[PolymorphicSerialization]
public partial record GetDocumentInformationExtractionIds(
    [property: DataMember(Order = 1)]
    int Skip,
    [property: DataMember(Order = 2)]
    int Take,
    [property: DataMember(Order = 3)]
    IEnumerable<string> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionIds"/> class.
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionIds"/> record with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    public GetDocumentInformationExtractionIds(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionIds"/> class.
    /// Initializes a new instance of the <see cref="GetDocumentInformationExtractionIds"/> record with default values.
    /// </summary>
    public GetDocumentInformationExtractionIds()
        : this(0, 0, [])
    {
    }

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => new GetDocumentInformationExtractionIds(Skip + Take, Take);

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<string>)results };
}