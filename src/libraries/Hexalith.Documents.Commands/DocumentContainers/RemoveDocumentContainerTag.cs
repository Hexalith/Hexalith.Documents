// <copyright file="RemoveDocumentContainerTag.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an command that occurs when a tag is removed from a document container.
/// </summary>
/// <param name="Id">The identifier of the document container.</param>
/// <param name="Key">The identifier of the tag that was removed.</param>
/// <param name="Value">The value of the tag that was removed.</param>
[PolymorphicSerialization]
public partial record RemoveDocumentContainerTag(
    string Id,
    [property: DataMember(Order = 2)]
    string Key,
    [property: DataMember(Order = 3)]
    string? Value)
    : DocumentContainerCommand(Id);