﻿namespace Hexalith.Documents.Events.DocumentInformationExtractions;

using System.Runtime.Serialization;
using Hexalith.PolymorphicSerialization;

/// <summary>
/// Represents an event that is raised when the description of a file text extraction mode is changed.
/// </summary>
/// <param name="Id">The unique identifier of the extraction mode.</param>
/// <param name="Name">The name of the extraction mode.</param>
/// <param name="Description">The new description of the extraction mode.</param>
[PolymorphicSerialization]
public partial record DocumentInformationExtractionDescriptionChanged(
    string Id,
    [property: DataMember(Order = 2)]
    string Name,
    [property: DataMember(Order = 3)]
    string Description) : DocumentInformationExtractionEvent(Id)
{
}