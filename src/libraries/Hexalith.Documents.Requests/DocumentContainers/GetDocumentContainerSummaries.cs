// <copyright file="GetDocumentContainerSummaries.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of document container with essential information.
/// </summary>
/// <param name="Skip">The number of document container summaries to skip.</param>
/// <param name="Take">The number of document container summaries to take.</param>
/// <param name="Search">The search to apply to the document container summaries.</param>
/// <param name="Ids">The list of document container summary IDs.</param>
/// <param name="Results">The list of document container summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentContainerSummaries(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] string? Search,
    [property: DataMember(Order = 4)] IEnumerable<string> Ids,
    [property: DataMember(Order = 5)] IEnumerable<DocumentContainerSummaryViewModel> Results) : ISearchChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerSummaries"/> class.
    /// </summary>
    public GetDocumentContainerSummaries()
        : this(0, 0, null, [], [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerSummaries"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document container summaries to skip.</param>
    /// <param name="take">The number of document container summaries to take.</param>
    /// <param name="search">The search to apply to the document container summaries.</param>
    public GetDocumentContainerSummaries(int skip, int take, string? search = null)
        : this(skip, take, search, Array.Empty<string>(), Array.Empty<DocumentContainerSummaryViewModel>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentContainerSummaries"/> class with specified skip, take, and IDs values.
    /// </summary>
    /// <param name="ids">The list of document container summary IDs.</param>
    public GetDocumentContainerSummaries(IEnumerable<string> ids)
        : this(0, 0, null, ids, Array.Empty<DocumentContainerSummaryViewModel>())
    {
    }

    /// <summary>
    /// Gets the aggregate ID of the document command.
    /// </summary>
    public static string AggregateId => DocumentDomainHelper.DocumentContainerAggregateName;

    /// <summary>
    /// Gets the aggregate name of the document command.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentContainerAggregateName;

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results)
        => this with { Results = (IEnumerable<DocumentContainerSummaryViewModel>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => this with { Skip = Skip + Take };
}