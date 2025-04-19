// <copyright file="GetFileTypeExports.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.FileTypes;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request to get file type exports with pagination support.
/// </summary>
/// <param name="Skip">The number of items to skip.</param>
/// <param name="Take">The number of items to take.</param>
/// <param name="Results">The collection of file type exports.</param>
[PolymorphicSerialization]
public partial record GetFileTypeExports(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<FileType> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeExports"/> class.
    /// </summary>
    public GetFileTypeExports()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileTypeExports"/> class.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    public GetFileTypeExports(int skip, int take)
        : this(skip, take, [])
    {
    }

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
    public IChunkableRequest CreateNextChunkRequest() => new GetFileTypeExports(Skip + Take, Take);

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<FileType>)results };
}