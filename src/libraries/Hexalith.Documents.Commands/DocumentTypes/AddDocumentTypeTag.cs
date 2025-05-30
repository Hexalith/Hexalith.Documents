// <copyright file="AddDocumentTypeTag.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to add a metadata tag to a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Key">The key or name of the tag.</param>
/// <param name="Value">The value associated with the tag.</param>
/// <param name="Unique">Indicates whether this tag's value must be unique across all documents of this type.</param>
/// <remarks>
/// Tags provide a flexible way to add metadata to document types.
/// When a tag is marked as unique, the system ensures that no two documents of this type can have the same value for this tag.
/// </remarks>
[PolymorphicSerialization]
public partial record AddDocumentTypeTag(
    string Id,
    [property: DataMember(Order = 2)] string Key,
    [property: DataMember(Order = 3)] string Value,
    [property: DataMember(Order = 4)] bool Unique = false) : DocumentTypeCommand(Id);