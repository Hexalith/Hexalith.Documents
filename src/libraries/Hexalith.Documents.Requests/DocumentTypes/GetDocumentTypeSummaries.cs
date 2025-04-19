// <copyright file="GetDocumentTypeSummaries.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of document type summaries with pagination and search capabilities.
/// </summary>
/// <param name="Skip">The number of document type summaries to skip.</param>
/// <param name="Take">The number of document type summaries to take.</param>
/// <param name="Search">The search term to filter document type summaries.</param>
/// <param name="Ids">The list of document type IDs to include in the results.</param>
/// <param name="Results">The list of document type summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentTypeSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Search,
    [property: DataMember(Order = 4)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<DocumentTypeSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeSummaries"/> class.
    /// </summary>
    public GetDocumentTypeSummaries()
        : this(0, 0, null, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeSummaries"/> class.
    /// </summary>
    /// <param name="skip">The number of document type summaries to skip.</param>
    /// <param name="take">The number of document type summaries to take.</param>
    /// <param name="search">The search term to filter document type summaries.</param>
    public GetDocumentTypeSummaries(int skip, int take, string? search = null)
        : this(skip, take, search, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeSummaries"/> class.
    /// </summary>
    /// <param name="ids">The list of document type IDs to include in the results.</param>
    public GetDocumentTypeSummaries(IEnumerable<string> ids)
        : this(0, 0, null, ids, [])
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document type request.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentTypeAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document type request.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentTypeAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<DocumentTypeSummaryViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take, Results = [] };
}