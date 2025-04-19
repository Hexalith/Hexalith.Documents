// <copyright file="DocumentRouting.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Documents;

using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Represents the routing information for a document, defining the sender, recipient, and copy recipients.
/// </summary>
/// <param name="FromContactId">The unique identifier of the sender.</param>
/// <param name="ToContactIds">The unique identifier of the recipient.</param>
/// <param name="CopyToContactIds">The collection of unique identifiers of the copy recipients.</param>
[DataContract]
public record DocumentRouting(
    [property:DataMember(Order = 1)]
    string FromContactId,
    [property: DataMember(Order = 2)]
    IEnumerable<string> ToContactIds,
    [property: DataMember(Order = 3)]
    IEnumerable<string> CopyToContactIds);