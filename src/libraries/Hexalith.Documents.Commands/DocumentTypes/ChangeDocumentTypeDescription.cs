// <copyright file="ChangeDocumentTypeDescription.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to update the name and description of an existing document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type to update.</param>
/// <param name="Name">The new display name for the document type.</param>
/// <param name="Description">The new description detailing the document type's purpose.</param>
/// <remarks>
/// This command allows modification of the document type's descriptive properties
/// without affecting its other configurations like supported file types or data extractions.
/// </remarks>
[PolymorphicSerialization]
public partial record ChangeDocumentTypeDescription(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string? Description) : DocumentTypeCommand(Id);