// <copyright file="DocumentContainerTagAdded.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a tag is added to a document container.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="Key">The identifier of the tag being added.</param>
/// <param name="Value">The value associated with the tag.</param>
/// <param name="Unique">A value indicating whether the tag is unique.</param>
[PolymorphicSerialization]
public partial record DocumentContainerTagAdded(
    string Id,
    [property: DataMember(Order = 2)]
    string Key,
    [property: DataMember(Order = 3)]
    string Value,
    [property: DataMember(Order = 4)]
    bool Unique)
    : DocumentContainerEvent(Id)
{
}