// <copyright file="DataManagementRequest.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DataManagements;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a request for data export.
/// </summary>
/// <param name="Id">The unique identifier of the data management request.</param>
[PolymorphicSerialization]
public abstract partial record DataManagementRequest([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate identifier.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the name of the aggregate.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DataManagementAggregateName;
}