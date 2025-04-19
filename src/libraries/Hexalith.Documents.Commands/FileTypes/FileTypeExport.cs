// <copyright file="FileTypeExport.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to export file types.
/// </summary>
/// <param name="UserId">The identifier of the user requesting the export.</param>
[PolymorphicSerialization]
public partial record FileTypeExport([property: DataMember(Order = 2)] string UserId) : FileTypeCommand(AggregateName);
