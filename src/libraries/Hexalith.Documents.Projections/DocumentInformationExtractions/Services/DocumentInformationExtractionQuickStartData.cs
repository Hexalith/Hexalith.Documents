// <copyright file="DocumentInformationExtractionQuickStartData.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Services;

using Hexalith.Documents.Commands.DocumentInformationExtractions;

/// <summary>
/// Provides demo document information extraction data for testing and demonstration purposes.
/// This static class contains sample document information extractions that can be used during development and testing.
/// </summary>
public static class DocumentInformationExtractionQuickStartData
{
    private const string _outputFormat = """
                    {
                        ""Name"": ""EmailActors"",
                        ""Description"": ""Extract email actors"",
                        ""Model"": ""GPT-4o"",
                        ""Instructions"": ""Extracts the sender, the recipients and copy to emails from the document."",
                        ""Fields"": [
                            {
                                ""Name"": ""Sender"",
                                ""Description"": ""Get the email sender.""
                            },
                            {
                                ""Name"": ""Recipients"",
                                ""Description"": ""Get the email recipients.""
                            }
                        ]
                    }
                    """;

    /// <summary>
    /// Gets a collection of sample document information extraction details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="AddDocumentInformationExtraction"/> containing predefined document information extractions.
    /// </value>
    public static IEnumerable<AddDocumentInformationExtraction> Data => [Excel];

    /// <summary>
    /// Gets the details for the Excel document information extraction.
    /// </summary>
    internal static AddDocumentInformationExtraction Excel => new(
        "EmailActors",
        "Extract email actors",
        "GPT-4o",
        "Extracts the sender, the recipients and copy to emails from the document.",

        string.Empty,
        _outputFormat,
        "Get the email sender and recipients of the email.",
        "claude-sonnet-3.5",
        "Check that the sender is defined and that the json format is correct.",
        null);
}