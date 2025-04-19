// <copyright file="DocumentTagRemoved.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.Documents;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents a document tag removed event.
/// </summary>
/// <param name="Id">The document ID.</param>
/// <param name="Key">The tag key.</param>
[PolymorphicSerialization]
public partial record DocumentTagRemoved(string Id, [property: DataMember(Order = 2)] string Key)
    : DocumentEvent(Id)
{
}