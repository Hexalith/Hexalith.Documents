// <copyright file="DocumentInformationExtractionAdded.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Events.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a new file text extraction mode is created.
/// </summary>
/// <param name="Id">The unique identifier for the extraction mode.</param>
/// <param name="Name">The name of the extraction mode.</param>
/// <param name="Model"></param>
/// <param name="SystemMessage"></param>
/// <param name="OutputFormat"></param>
/// <param name="OutputSample"></param>
/// <param name="Instructions">The instructions used for text extraction.</param>
/// <param name="ValidationModel"></param>
/// <param name="ValidationInstructions"></param>
/// <param name="Comments"></param>
[PolymorphicSerialization]
public partial record DocumentInformationExtractionAdded(
    string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Model,
    [property: DataMember(Order = 4)] string SystemMessage,
    [property: DataMember(Order = 5)] string OutputFormat,
    [property: DataMember(Order = 6)] string OutputSample,
    [property: DataMember(Order = 7)] string Instructions,
    [property: DataMember(Order = 8)] string ValidationModel,
    [property: DataMember(Order = 9)] string ValidationInstructions,
    [property: DataMember(Order = 10)] string? Comments)
    : DocumentInformationExtractionEvent(Id)
{
}