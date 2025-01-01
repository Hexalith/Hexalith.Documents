namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of document partition with essential information.
/// </summary>
/// <param name="Skip">The number of document partition summaries to skip.</param>
/// <param name="Take">The number of document partition summaries to take.</param>
/// <param name="Result">The list of document partition summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentStorageSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentStorageSummaryViewModel> Result)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageSummaries"/> class.
    /// </summary>
    public GetDocumentStorageSummaries()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document partition summaries to skip.</param>
    /// <param name="take">The number of document partition summaries to take.</param>
    public GetDocumentStorageSummaries(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentStorageAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentStorageAggregateName;
}