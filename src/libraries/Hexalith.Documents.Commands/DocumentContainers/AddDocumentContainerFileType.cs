// <copyright file="AddDocumentContainerFileType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an command that occurs when a file type is added to a document container's supported file types.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="FileTypeId">The identifier of the file type being added.</param>
[PolymorphicSerialization]
public partial record AddDocumentContainerFileType(
    string Id,
    [property: DataMember(Order = 2)]
    string FileTypeId)
    : DocumentContainerCommand(Id);