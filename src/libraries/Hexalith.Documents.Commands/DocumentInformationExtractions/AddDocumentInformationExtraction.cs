// <copyright file="AddDocumentInformationExtraction.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Commands.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.PolymorphicSerializations;

/// <summary>
/// Represents an event that is raised when a new file text extraction mode is created.
/// </summary>
/// <param name="Id">The unique identifier for the extraction mode.</param>
/// <param name="Name">The name of the extraction mode.</param>
/// <param name="Model">The model used for text extraction.</param>
/// <param name="SystemMessage">The system message.</param>
/// <param name="OutputFormat">The output format.</param>
/// <param name="OutputSample">The output sample.</param>
/// <param name="Instructions">The instructions used for text extraction.</param>
/// <param name="ValidationModel">The validation model.</param>
/// <param name="ValidationInstructions">The validation instructions.</param>
/// <param name="Comments">The comments.</param>
[PolymorphicSerialization]
public partial record AddDocumentInformationExtraction(
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
    : DocumentInformationExtractionCommand(Id)
{
}