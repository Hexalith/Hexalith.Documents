namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of file type with essential information.
/// </summary>
/// <param name="Skip">The number of file type summaries to skip.</param>
/// <param name="Take">The number of file type summaries to take.</param>
/// <param name="Result">The list of file type summaries.</param>
[PolymorphicSerialization]
public partial record GetFileTypeSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<FileTypeSummaryViewModel> Result)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummaries"/> class.
    /// </summary>
    public GetFileTypeSummaries()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of file type summaries to skip.</param>
    /// <param name="take">The number of file type summaries to take.</param>
    public GetFileTypeSummaries(int skip, int take)
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
}