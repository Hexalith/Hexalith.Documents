// <copyright file="FileTypeContentTypeChanged.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when the content type of a file type is changed.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="ContentType">The new content type for the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeContentTypeChanged(
    string Id,
    [property: DataMember(Order = 3)] string ContentType)
    : FileTypeEvent(Id)
{
}