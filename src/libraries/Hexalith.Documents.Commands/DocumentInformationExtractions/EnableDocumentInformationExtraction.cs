// <copyright file="EnableDocumentInformationExtraction.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentInformationExtractions;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a file text extraction mode is enabled.
/// </summary>
/// <param name="Id">The unique identifier of the extraction mode that was enabled.</param>
[PolymorphicSerialization]
public partial record EnableDocumentInformationExtraction(string Id)
    : DocumentInformationExtractionCommand(Id);