// <copyright file="GetDocumentSummaries.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of document with essential information.
/// </summary>
/// <param name="Skip">The number of document summaries to skip.</param>
/// <param name="Take">The number of document summaries to take.</param>
/// <param name="Search"></param>
/// <param name="Ids"></param>
/// <param name="Results">The list of document summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 4)] string? Search,
    [property: DataMember(Order = 3)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<DocumentSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentSummaries"/> class.
    /// </summary>
    public GetDocumentSummaries()
        : this(0, 0, null, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document summaries to skip.</param>
    /// <param name="take">The number of document summaries to take.</param>
    /// <param name="search">The search criteria for the document summaries.</param>
    public GetDocumentSummaries(int skip, int take, string? search)
        : this(skip, take, search, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentSummaries"/> class with specified document IDs.
    /// </summary>
    /// <param name="ids">The list of document IDs.</param>
    public GetDocumentSummaries(IEnumerable<string> ids)
        : this(0, 0, null, ids, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results)
        => this with { Results = (IEnumerable<DocumentSummaryViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take, Results = [] };
}