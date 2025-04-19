// <copyright file="FileTypeFileToTextConverterChanged.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that sets the file-to-text converter for a file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="FileToTextConverter">The file-to-text converter to be set.</param>
[PolymorphicSerialization]
public partial record FileTypeFileToTextConverterChanged(
    string Id,
    [property: DataMember(Order = 2)] string? FileToTextConverter)
    : FileTypeEvent(Id);