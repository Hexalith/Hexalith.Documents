// <copyright file="DocumentTag.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.ValueObjects;

using System.Runtime.Serialization;

/// <summary>
/// Represents a tag associated with a document.
/// </summary>
/// <param name="Key">The key of the tag.</param>
/// <param name="Value">The value of the tag.</param>
/// <param name="Unique">Indicates whether the tag is unique.</param>
[DataContract]
public record DocumentTag
(
    [property: DataMember] string Key,
    [property: DataMember] string? Value,
    [property: DataMember] bool Unique)
{
}