namespace Hexalith.Documents.Requests.DataExports;

using System.Runtime.Serialization;

using Hexalith.Documents.Domain;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents a request for a list of summaries of data export with essential information.
/// </summary>
/// <param name="Skip">The number of data export summaries to skip.</param>
/// <param name="Take">The number of data export summaries to take.</param>
/// <param name="Result">The list of data export summaries.</param>
[PolymorphicSerialization]
public partial record GetDataExportSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DataExportSummaryViewModel> Result)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataExportSummaries"/> class.
    /// </summary>
    public GetDataExportSummaries()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataExportSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of data export summaries to skip.</param>
    /// <param name="take">The number of data export summaries to take.</param>
    public GetDataExportSummaries(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DataExportAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DataExportAggregateName;
}