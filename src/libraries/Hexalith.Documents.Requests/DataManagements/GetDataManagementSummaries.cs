// <copyright file="GetDataManagementSummaries.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DataManagements;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of data export with essential information.
/// </summary>
/// <param name="Skip">The number of data export summaries to skip.</param>
/// <param name="Take">The number of data export summaries to take.</param>
/// <param name="Search"></param>
/// <param name="Ids">The list of data export summary IDs.</param>
/// <param name="Results">The list of data export summaries.</param>
[PolymorphicSerialization]
public partial record GetDataManagementSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Search,
    [property: DataMember(Order = 4)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<DataManagementSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataManagementSummaries"/> class.
    /// </summary>
    public GetDataManagementSummaries()
        : this(0, 0, null, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataManagementSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of data export summaries to skip.</param>
    /// <param name="take">The number of data export summaries to take.</param>
    /// <param name="search">The search to apply to the data export summaries.</param>
    public GetDataManagementSummaries(int skip, int take, string? search = null)
        : this(skip, take, search, Array.Empty<string>(), Array.Empty<DataManagementSummaryViewModel>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataManagementSummaries"/> class with specified skip, take, and IDs values.
    /// </summary>
    /// <param name="skip">The number of data export summaries to skip.</param>
    /// <param name="take">The number of data export summaries to take.</param>
    /// <param name="ids">The list of data export summary IDs.</param>
    public GetDataManagementSummaries(int skip, int take, IEnumerable<string> ids)
        : this(skip, take, null, ids, Array.Empty<DataManagementSummaryViewModel>())
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DataManagementAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DataManagementAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results)
        => this with { Results = (IEnumerable<DataManagementSummaryViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take };
}