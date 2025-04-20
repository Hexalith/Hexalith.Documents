// <copyright file="AddDocumentTypeDataExtraction.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentTypes;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Command to associate a data extraction configuration with a document type.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="DataInformationExtractionId">The identifier of the data extraction configuration to associate with the document type.</param>
/// <remarks>
/// This command enables automatic data extraction capabilities for documents of this type
/// by linking them to a specific data extraction configuration.
/// </remarks>
[PolymorphicSerialization]
public partial record AddDocumentTypeDataExtraction(
    string Id,
    [property: DataMember(Order = 2)]
    string DataInformationExtractionId) : DocumentTypeCommand(Id);