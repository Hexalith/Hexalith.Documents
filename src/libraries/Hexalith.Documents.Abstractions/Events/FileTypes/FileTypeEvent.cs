// <copyright file="FileTypeEvent.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.Documents;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a base class for file type events.
/// </summary>
/// <param name="Id">The identifier of the file type.</param>
[PolymorphicSerialization]
public abstract partial record FileTypeEvent([property: DataMember(Order = 1)] string Id)
{
    /// <summary>
    /// Gets the aggregate ID.
    /// </summary>
    public string AggregateId => Id;

    /// <summary>
    /// Gets the aggregate name.
    /// </summary>
    public static string AggregateName => DocumentDomainHelper.FileTypeAggregateName;
}