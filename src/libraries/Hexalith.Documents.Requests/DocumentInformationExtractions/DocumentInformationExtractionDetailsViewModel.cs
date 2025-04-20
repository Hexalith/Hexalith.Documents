// <copyright file="DocumentInformationExtractionDetailsViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentInformationExtractions;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents the details of a document information extraction.
/// </summary>
/// <param name="Id">The unique identifier of the document information extraction.</param>
/// <param name="Name">The name of the document information extraction.</param>
/// <param name="Model">The AI model used for the extraction.</param>
/// <param name="SystemMessage">The system message that guides the AI in extraction.</param>
/// <param name="OutputFormat">The expected format of the extraction output.</param>
/// <param name="OutputSample">A sample of the expected output format.</param>
/// <param name="Instructions">Instructions for performing the extraction.</param>
/// <param name="ValidationModel">The AI model used for validating extractions.</param>
/// <param name="ValidationInstructions">Instructions for validating the extracted information.</param>
/// <param name="Comments">Additional comments about the extraction configuration.</param>
/// <param name="Disabled">Indicates whether this extraction configuration is disabled.</param>
[DataContract]
public sealed record DocumentInformationExtractionDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 3)] string Model,
    [property: DataMember(Order = 4)] string SystemMessage,
    [property: DataMember(Order = 5)] string OutputFormat,
    [property: DataMember(Order = 6)] string OutputSample,
    [property: DataMember(Order = 7)] string Instructions,
    [property: DataMember(Order = 8)] string ValidationModel,
    [property: DataMember(Order = 9)] string ValidationInstructions,
    [property: DataMember(Order = 10)] string? Comments,
    [property: DataMember(Order = 11)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}