namespace Hexalith.Documents.Projections.DocumentInformationExtractions.Services;

using Hexalith.Documents.Commands.DocumentInformationExtractions;

/// <summary>
/// Provides demo document information extraction data for testing and demonstration purposes.
/// This static class contains sample document information extractions that can be used during development and testing.
/// </summary>
public static class DocumentInformationExtractionQuickStartData
{
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
        "Get the email sender and recipients of the email.");
}