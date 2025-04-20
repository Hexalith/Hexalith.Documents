// <copyright file="GetDocumentIds.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get document IDs with pagination.
/// </summary>
/// <param name="Skip">The number of document IDs to skip.</param>
/// <param name="Take">The number of document IDs to take.</param>
/// <param name="Results">The collection of document IDs.</param>
[PolymorphicSerialization]
public partial record GetDocumentIds(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<string> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentIds"/> class.
    /// </summary>
    public GetDocumentIds()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDocumentIds"/> class.
    /// </summary>
    /// <param name="skip">The number of document IDs to skip.</param>
    /// <param name="take">The number of document IDs to take.</param>
    public GetDocumentIds(int skip, int take)
        : this(skip, take, [])
    {
    }

    /// <inheritdoc/>
    IEnumerable<object>? ICollectionRequest.Results => Results;

    /// <inheritdoc/>
    public IChunkableRequest CreateNextChunkRequest() => new GetDocumentIds(Skip + Take, Take);

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = results.Cast<string>() };
}