// <copyright file="GetDocumentExports.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of documents with essential information.
/// </summary>
/// <param name="Skip">The number of document summaries to skip.</param>
/// <param name="Take">The number of document summaries to take.</param>
/// <param name="Results">The list of document summaries.</param>
[PolymorphicSerialization]
public partial record GetDocumentExports(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentSummaryViewModel> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentExports"/> class.
    /// </summary>
    public GetDocumentExports()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentExports"/> class with specified skip and take values.
    /// </summary>
    /// <param name="skip">The number of document summaries to skip.</param>
    /// <param name="take">The number of document summaries to take.</param>
    public GetDocumentExports(int skip, int take)
        : this(skip, take, [])
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
    IEnumerable<object>? ICollectionRequest.Results { get; }

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => throw new NotImplementedException();

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => throw new NotImplementedException();
}