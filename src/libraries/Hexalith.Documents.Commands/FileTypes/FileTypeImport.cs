// <copyright file="FileTypeImport.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.FileTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to import file types from a document.
/// </summary>
/// <param name="DocumentId">The identifier of the document containing the file types to import.</param>
[PolymorphicSerialization]
public partial record FileTypeImport([property: DataMember(Order = 2)] string DocumentId) : FileTypeCommand(AggregateName);
