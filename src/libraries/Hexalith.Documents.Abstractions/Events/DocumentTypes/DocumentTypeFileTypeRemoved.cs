// <copyright file="DocumentTypeFileTypeRemoved.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a file type is removed from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="FileTypeId">The identifier of the file type that was removed from the document type.</param>
[PolymorphicSerialization]
public partial record DocumentTypeFileTypeRemoved(
    string Id,
    [property: DataMember(Order = 2)]
    string FileTypeId)
    : DocumentTypeEvent(Id)
{
}