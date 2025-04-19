// <copyright file="FileTypeFileExtensionChanged.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that occurs when the file extension of a file type is changed.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="FileExtension">The new file extension for the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeFileExtensionChanged(
    string Id,
    [property: DataMember(Order = 3)] string FileExtension)
    : FileTypeEvent(Id);