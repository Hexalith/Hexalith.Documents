﻿// <copyright file="AddFileType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to add a new file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="Name">The name of the file type.</param>
/// <param name="ContentType">The primary content type of the file.</param>
/// <param name="OtherContentTypes">Other possible content types for the file.</param>
/// <param name="FileExtension">The file extension associated with this file type.</param>
/// <param name="Comments">The description of the file type.</param>
/// <param name="FileToTextConverter">The converter used to convert the file to text.</param>
[PolymorphicSerialization]
public partial record AddFileType(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string ContentType,
    [property: DataMember(Order = 4)] IEnumerable<string> OtherContentTypes,
    [property: DataMember(Order = 5)] string FileExtension,
    [property: DataMember(Order = 7)] string? Comments,
    [property: DataMember(Order = 8)] string? FileToTextConverter)
    : FileTypeCommand(Id);