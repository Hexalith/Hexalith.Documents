// <copyright file="FileTypeDisabled.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.FileTypes;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a file type is disabled.
/// </summary>
/// <param name="Id">The unique identifier of the file type.</param>
[PolymorphicSerialization]
public partial record FileTypeDisabled(string Id) : FileTypeEvent(Id);