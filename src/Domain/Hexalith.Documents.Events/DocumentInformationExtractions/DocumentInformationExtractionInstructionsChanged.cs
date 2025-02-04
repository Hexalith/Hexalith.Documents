﻿namespace Hexalith.Documents.Events.DocumentInformationExtractions;

using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when the extraction instructions of a file text extraction mode are changed.
/// </summary>
/// <param name="Id">The unique identifier of the extraction mode.</param>
/// <param name="Instructions">The new instructions for text extraction.</param>
[PolymorphicSerialization]
public partial record DocumentInformationInstructionsChanged(string Id, string Instructions) : DocumentInformationExtractionEvent(Id)
{
}