// <copyright file="DisableDocumentType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentTypes;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to disable an existing document type in the system.
/// </summary>
/// <param name="Id">The unique identifier of the document type to disable.</param>
/// <remarks>
/// When a document type is disabled, it remains in the system but cannot be used for new documents.
/// Existing documents of this type are not affected but no new documents can be created with this type.
/// </remarks>
[PolymorphicSerialization]
public partial record DisableDocumentType(string Id) : DocumentTypeCommand(Id);
