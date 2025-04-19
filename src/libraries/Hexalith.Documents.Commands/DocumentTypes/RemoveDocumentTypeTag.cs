// <copyright file="RemoveDocumentTypeTag.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to remove a metadata tag from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Key">The key or name of the tag to remove.</param>
/// <param name="Value">The specific value of the tag to remove.</param>
/// <remarks>
/// This command removes a specific tag and its value from the document type's metadata.
/// After removal, the tag will no longer be associated with new documents of this type.
/// Existing documents retain their tags but the removed tag cannot be used for new documents.
/// </remarks>
[PolymorphicSerialization]
public partial record RemoveDocumentTypeTag(
    string Id,
    [property: DataMember(Order = 2)]
    string Key,
    [property: DataMember(Order = 2)]
    string Value)
    : DocumentTypeCommand(Id)
{
}