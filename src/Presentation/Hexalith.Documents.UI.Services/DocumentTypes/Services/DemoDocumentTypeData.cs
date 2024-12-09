namespace Hexalith.Documents.UI.Pages.DocumentTypes.Services;

using Hexalith.Documents.UI.Services.DocumentTypes.ViewModels;

/// <summary>
/// Provides demo document type data for testing and demonstration purposes.
/// This static class contains sample document types that can be used during development and testing.
/// </summary>
public static class DemoDocumentTypeData
{
    /// <summary>
    /// Gets a collection of sample document type details.
    /// </summary>
    /// <value>
    /// An enumerable collection of <see cref="DocumentTypeDetails"/> containing predefined document types.
    /// </value>
    internal static IEnumerable<DocumentTypeDetailsViewModel> Data => [Triage];

    /// <summary>
    /// Gets the details for the Excel document type.
    /// </summary>
    internal static DocumentTypeDetailsViewModel Triage => new(
        "Triage",
        "Triage",
        null,
        false);
}