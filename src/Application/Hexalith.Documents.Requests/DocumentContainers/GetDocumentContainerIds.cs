namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request to get document container IDs with pagination.
/// </summary>
/// <param name="Skip">The number of items to skip.</param>
/// <param name="Take">The number of items to take.</param>
/// <param name="Result">The collection of document container IDs.</param>
[PolymorphicSerialization]
public partial record GetDocumentContainerIds(
    [property: DataMember(Order = 1)]
    int Skip,
    [property: DataMember(Order = 2)]
    int Take,
    [property: DataMember(Order = 3)]
    IEnumerable<string> Result)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerIds"/> class.
    /// Initializes a new instance of the <see cref="GetDocumentContainerIds"/> record with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    public GetDocumentContainerIds(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerIds"/> class.
    /// Initializes a new instance of the <see cref="GetDocumentContainerIds"/> record with default values.
    /// </summary>
    public GetDocumentContainerIds()
        : this(0, 0, [])
    {
    }
}