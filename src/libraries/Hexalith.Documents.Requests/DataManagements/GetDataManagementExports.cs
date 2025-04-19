// <copyright file="GetDataManagementExports.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DataManagements;

using System.Runtime.Serialization;

using Hexalith.Application.Requests;
using Hexalith.Documents.DataManagements;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for a list of summaries of data export with essential information.
/// </summary>
/// <param name="Skip">The number of data export summaries to skip.</param>
/// <param name="Take">The number of data export summaries to take.</param>
/// <param name="Results">The collection of data export summaries.</param>
[PolymorphicSerialization]
public partial record GetDataManagementExports(
    [property: DataMember(Order = 1)] int Skip,
    [property: DataMember(Order = 2)] int Take,
    [property: DataMember(Order = 3)] IEnumerable<DataManagement> Results) : IChunkableRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataManagementExports"/> class.
    /// </summary>
    public GetDataManagementExports()
        : this(0, 0, [])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDataManagementExports"/> class.
    /// </summary>
    /// <param name="skip">The number of data export summaries to skip.</param>
    /// <param name="take">The number of data export summaries to take.</param>
    public GetDataManagementExports(int skip, int take)
        : this(skip, take, [])
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
    public IChunkableRequest CreateNextChunkRequest() => new GetDataManagementExports(Skip + Take, Take);

    /// <inheritdoc/>
    public ICollectionRequest CreateResults(IEnumerable<object> results) => this with { Results = (IEnumerable<DataManagement>)results };
}