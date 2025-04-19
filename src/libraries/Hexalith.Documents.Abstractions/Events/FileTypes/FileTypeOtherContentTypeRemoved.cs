// <copyright file="FileTypeOtherContentTypeRemoved.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when an alternative content type is removed from a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="OtherContentType">The content type being removed from the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeOtherContentTypeRemoved(
    string Id,
    [property: DataMember(Order = 2)] string OtherContentType)
    : FileTypeEvent(Id)
{
}