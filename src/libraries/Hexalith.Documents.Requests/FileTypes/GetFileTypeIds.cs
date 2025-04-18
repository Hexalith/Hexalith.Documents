namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get file type IDs with pagination.
/// </summary>
/// <param name="Skip">The number of items to skip.</param>
/// <param name="Take">The number of items to take.</param>
/// <param name="Results">The collection of file type IDs.</param>
[PolymorphicSerialization]
public partial record GetFileTypeIds(
    [property: DataMember(Order = 1)]
    int Skip,
    [property: DataMember(Order = 2)]
    int Take,
    [property: DataMember(Order = 3)]
    IEnumerable<string> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeIds"/> class.
    /// Initializes a new instance of the <see cref="GetFileTypeIds"/> record with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    public GetFileTypeIds(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeIds"/> class.
    /// Initializes a new instance of the <see cref="GetFileTypeIds"/> record with default values.
    /// </summary>
    public GetFileTypeIds()
        : this(0, 0, [])
    {
    }

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => new GetFileTypeIds(Skip + Take, Take);

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<string>)results };
}