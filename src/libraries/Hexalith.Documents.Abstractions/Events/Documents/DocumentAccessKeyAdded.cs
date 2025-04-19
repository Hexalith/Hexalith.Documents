// <copyright file="DocumentAccessKeyAdded.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.Documents;

using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when an access key is added to a document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="AccessKey">The access key information being added to the document.</param>
[PolymorphicSerialization]
public partial record DocumentAccessKeyAdded(
    string Id,
    [property: DataMember(Order = 2)] DocumentAccessKey AccessKey) : DocumentEvent(Id)
{
}