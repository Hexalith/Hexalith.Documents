// <copyright file="ChangeFileTypeFileExtension.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to change the file extension of an existing file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
/// <param name="FileExtension">The new file extension to set.</param>
[PolymorphicSerialization]
public partial record ChangeFileTypeFileExtension(
    string Id,
    [property: DataMember(Order = 3)] string FileExtension)
    : FileTypeCommand(Id);