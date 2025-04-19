// <copyright file="ChangeFileTypeContentType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to change the content type of an existing file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="ContentType">The new content type to set.</param>
[PolymorphicSerialization]
public partial record ChangeFileTypeContentType(
    string Id,
    [property: DataMember(Order = 3)] string ContentType)
    : FileTypeCommand(Id);