﻿// <copyright file="DocumentRequest.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a base class for document requests.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
[PolymorphicSerialization]
public abstract partial record DocumentRequest([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate ID of the document request.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name of the document request.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentAggregateName;
}