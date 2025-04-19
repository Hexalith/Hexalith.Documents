// <copyright file="DocumentInformationExtractionInstructionsChanged.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentInformationExtractions;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when the extraction instructions of a file text extraction mode are changed.
/// </summary>
/// <param name="Id">The unique identifier of the extraction mode.</param>
/// <param name="Instructions">The new instructions for text extraction.</param>
[PolymorphicSerialization]
public partial record DocumentInformationExtractionInstructionsChanged(string Id, string Instructions) : DocumentInformationExtractionEvent(Id);