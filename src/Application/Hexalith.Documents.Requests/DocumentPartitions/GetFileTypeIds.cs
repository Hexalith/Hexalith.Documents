namespace Hexalith.Documents.Requests.DocumentPartitions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get document partition IDs with pagination.
/// </summary>
/// <param name="Skip">The number of items to skip.</param>
/// <param name="Take">The number of items to take.</param>
/// <param name="Result">The collection of document partition IDs.</param>
[PolymorphicSerialization]
public partial record GetDocumentPartitionIds(
    [property: DataMember(Order = 1)]
    int Skip,
    [property: DataMember(Order = 2)]
    int Take,
    [property: DataMember(Order = 3)]
    IEnumerable<string> Result)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentPartitionIds"/> class.
    /// Initializes a new instance of the <see cref="GetDocumentPartitionIds"/> record with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    public GetDocumentPartitionIds(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentPartitionIds"/> class.
    /// Initializes a new instance of the <see cref="GetDocumentPartitionIds"/> record with default values.
    /// </summary>
    public GetDocumentPartitionIds()
        : this(0, 0, [])
    {
    }
}