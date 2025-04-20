// <copyright file="RemoveDocumentTypeDataExtraction.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to remove a data extraction configuration from a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="DataInformationExtractionId">The identifier of the data extraction configuration to remove.</param>
/// <remarks>
/// This command disassociates a data extraction configuration from the document type.
/// After removal, documents of this type will no longer use this extraction configuration for data processing.
/// </remarks>
[PolymorphicSerialization]
public partial record RemoveDocumentTypeDataExtraction(
    string Id,
    [property: DataMember(Order = 2)]
    string DataInformationExtractionId) : DocumentTypeCommand(Id);