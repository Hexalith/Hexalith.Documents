// <copyright file="GetFileTypeSummaries.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of file type summaries with pagination and search capabilities.
/// </summary>
/// <param name="Skip">The number of file type summaries to skip.</param>
/// <param name="Take">The number of file type summaries to take.</param>
/// <param name="Search">The search term to filter file type summaries.</param>
/// <param name="Ids">The list of file type IDs to include in the results.</param>
/// <param name="Results">The list of file type summaries.</param>
[PolymorphicSerialization]
public partial record GetFileTypeSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Search,
    [property: DataMember(Order = 4)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<FileTypeSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummaries"/> class.
    /// </summary>
    public GetFileTypeSummaries()
        : this(0, 0, null, [], []) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummaries"/> class.
    /// </summary>
    /// <param name="skip">The number of file type summaries to skip.</param>
    /// <param name="take">The number of file type summaries to take.</param>
    /// <param name="search">The search term to filter file type summaries.</param>
    public GetFileTypeSummaries(int skip, int take, string? search = null)
        : this(skip, take, search, [], []) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeSummaries"/> class.
    /// </summary>
    /// <param name="ids">The list of file type IDs to include in the results.</param>
    public GetFileTypeSummaries(IEnumerable<string> ids)
        : this(0, 0, null, ids, []) { }

    /// <summary>
    /// Gets the aggregate ID of the file type request.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.FileTypeAggregateName;

    /// <summary>
    /// Gets the aggregate name of the file type request.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.FileTypeAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take, Results = [] };

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<FileTypeSummaryViewModel>)results };
}