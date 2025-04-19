// <copyright file="DocumentTypeTagRemoved.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a tag is removed from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Key">The identifier of the tag that was removed.</param>
/// <param name="Value"></param>
[PolymorphicSerialization]
public partial record DocumentTypeTagRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string Key,
    [property: DataMember(Order = 2)]
    string Value)
    : DocumentTypeEvent(Id)
{
}