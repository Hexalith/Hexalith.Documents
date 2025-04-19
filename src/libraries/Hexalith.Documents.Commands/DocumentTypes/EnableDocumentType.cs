// <copyright file="EnableDocumentType.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentTypes;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to enable a previously disabled document type in the system.
/// </summary>
/// <param name="Id">The unique identifier of the document type to enable.</param>
/// <remarks>
/// When a document type is enabled, it becomes available for use with new documents.
/// This command restores full functionality to a previously disabled document type.
/// </remarks>
[PolymorphicSerialization]
public partial record EnableDocumentType(string Id) : DocumentTypeCommand(Id);
