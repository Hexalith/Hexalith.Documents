// <copyright file="DisableDocumentInformationExtraction.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentInformationExtractions;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a file text extraction mode is disabled.
/// </summary>
/// <param name="Id">The unique identifier of the extraction mode that was disabled.</param>
[PolymorphicSerialization]
public partial record DisableDocumentInformationExtraction(string Id) : DocumentInformationExtractionCommand(Id)
{
}