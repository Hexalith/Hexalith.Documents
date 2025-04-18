namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get document type IDs with pagination.
/// </summary>
/// <param name="Skip">The number of items to skip.</param>
/// <param name="Take">The number of items to take.</param>
/// <param name="Result">The collection of document type IDs.</param>
[PolymorphicSerialization]
public partial record GetDocumentTypeIds(
    [property: DataMember(Order = 1)]
    int Skip,
    [property: DataMember(Order = 2)]
    int Take,
    [property: DataMember(Order = 3)]
    IEnumerable<string> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeIds"/> class.
    /// Initializes a new instance of the <see cref="GetDocumentTypeIds"/> record with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    public GetDocumentTypeIds(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeIds"/> class.
    /// Initializes a new instance of the <see cref="GetDocumentTypeIds"/> record with default values.
    /// </summary>
    public GetDocumentTypeIds()
        : this(0, 0, [])
    {
    }

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => new GetDocumentTypeIds(Skip + Take, Take);

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<string>)results };
}