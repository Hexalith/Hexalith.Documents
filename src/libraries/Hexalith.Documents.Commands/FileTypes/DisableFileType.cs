﻿// <copyright file="DisableFileType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.FileTypes;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to disable an existing file type.
/// </summary>
/// <param name="Id">The unique identifier of the file type to disable.</param>
[PolymorphicSerialization]
public partial record DisableFileType(string Id) : FileTypeCommand(Id);
