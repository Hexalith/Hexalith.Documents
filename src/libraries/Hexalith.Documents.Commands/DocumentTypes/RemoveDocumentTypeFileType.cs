// <copyright file="RemoveDocumentTypeFileType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to remove a supported file type from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="FileTypeId">The identifier of the file type to remove from supported types.</param>
/// <remarks>
/// This command removes support for a specific file type from the document type.
/// After removal, new documents of this file type cannot be processed under this document type.
/// Existing documents are not affected by this change.
/// </remarks>
[PolymorphicSerialization]
public partial record RemoveDocumentTypeFileType(
    string Id,
    [property: DataMember(Order = 2)]
    string FileTypeId)
    : DocumentTypeCommand(Id)
{
}