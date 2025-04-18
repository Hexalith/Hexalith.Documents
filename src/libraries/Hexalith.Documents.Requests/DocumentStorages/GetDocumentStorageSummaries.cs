// <copyright file="GetDocumentStorageSummaries.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentStorages;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of document storage with essential information.
/// </summary>
/// <param name="Skip">The number of document storage summaries to skip.</param>
/// <param name="Take">The number of document storage summaries to take.</param>
/// <param name="Search"></param>
/// <param name="Ids">The list of document storage summary IDs.</param>
/// <param name="Results">The list of document storage summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentStorageSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Search,
    [property: DataMember(Order = 4)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<DocumentStorageSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageSummaries"/> class.
    /// </summary>
    public GetDocumentStorageSummaries()
        : this(0, 0, null, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document storage summaries to skip.</param>
    /// <param name="take">The number of document storage summaries to take.</param>
    /// <param name="search">The search to apply to the document storage summaries.</param>
    public GetDocumentStorageSummaries(int skip, int take, string? search = null)
        : this(skip, take, search, Array.Empty<string>(), Array.Empty<DocumentStorageSummaryViewModel>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentStorageSummaries"/> class with specified skip, take, and IDs values.
    /// </summary>
    /// <param name="ids">The list of document storage summary IDs.</param>
    public GetDocumentStorageSummaries(IEnumerable<string> ids)
        : this(0, 0, null, ids, Array.Empty<DocumentStorageSummaryViewModel>())
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

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results)
        => this with { Results = (IEnumerable<DocumentStorageSummaryViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take };
}