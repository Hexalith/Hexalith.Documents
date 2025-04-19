// <copyright file="DocumentTypeEvent.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a base class for document type events.
/// </summary>
/// <param name="Id">The identifier of the document type.</param>
[PolymorphicSerialization]
public abstract partial record DocumentTypeEvent([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate ID.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.DocumentTypeAggregateName;
}