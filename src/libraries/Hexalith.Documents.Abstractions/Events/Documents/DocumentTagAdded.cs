// <copyright file="DocumentTagAdded.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.Documents;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Event raised when a tag is added to a document.
/// </summary>
/// <param name="Id">The document identifier.</param>
/// <param name="Key">The tag identifier.</param>
/// <param name="Value">The tag value.</param>
/// <param name="Unique">A value indicating whether the tag is unique.</param>
[PolymorphicSerialization]
public partial record DocumentTagAdded(
    string Id,
    [property: DataMember(Order = 2)] string Key,
    [property: DataMember(Order = 3)] string Value,
    [property: DataMember(Order = 4)] bool Unique)
    : DocumentEvent(Id);