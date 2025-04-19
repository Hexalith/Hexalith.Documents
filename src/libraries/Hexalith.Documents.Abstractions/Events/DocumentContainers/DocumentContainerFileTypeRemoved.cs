// <copyright file="DocumentContainerFileTypeRemoved.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when a document type is removed from a document container's supported document types.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="DocumentTypeId">The identifier of the document type being removed.</param>
[PolymorphicSerialization]
public partial record DocumentContainerDocumentTypeRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string DocumentTypeId)
    : DocumentContainerEvent(Id)
{
}