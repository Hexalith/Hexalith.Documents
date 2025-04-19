// <copyright file="GetDocumentContainerExports.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.DocumentTypes;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of document type exports with pagination.
/// </summary>
/// <param name="Skip">The number of document type exports to skip.</param>
/// <param name="Take">The number of document type exports to take.</param>
/// <param name="Results">The list of document type exports.</param>
[PolymorphicSerialization]
public partial record GetDocumentTypeExports(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DocumentType> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeExports"/> class.
    /// </summary>
    public GetDocumentTypeExports()
        : this(0, 0, []) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentTypeExports"/> class.
    /// </summary>
    /// <param name="skip">The number of document type exports to skip.</param>
    /// <param name="take">The number of document type exports to take.</param>
    public GetDocumentTypeExports(int skip, int take)
        : this(skip, take, []) { }

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
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<DocumentType>)results };

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => new GetDocumentTypeExports(Skip + Take, Take);
}