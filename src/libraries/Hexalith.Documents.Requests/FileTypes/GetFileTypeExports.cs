namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.Domain;
using Hexalith.Documents.Domain.FileTypes;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get file type exports with pagination support.
/// </summary>
/// <param name="Skip">The number of items to skip.</param>
/// <param name="Take">The number of items to take.</param>
/// <param name="Results">The results of the file type exports.</param>
[PolymorphicSerialization]
public partial record GetFileTypeExports(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<FileType> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeExports"/> class with default values.
    /// </summary>
    /// <remarks>
    /// This constructor sets the <see cref="Skip"/> and <see cref="Take"/> properties to 0 and initializes an empty list for <see cref="Results"/>.
    /// </remarks>
    public GetFileTypeExports()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeExports"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    public GetFileTypeExports(int skip, int take)
        : this(skip, take, [])
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
    public IChunkableRequest CreateNextChunkRequest() => new GetFileTypeExports(Skip + Take, Take);

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<FileType>)results };
}