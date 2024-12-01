namespace Hexalith.Documents.UI.Pages.Documents.Services;

using Hexalith.Documents.UI.Components.Documents.ViewModels;

/// <summary>
/// Provides demo document data for testing and demonstration purposes.
/// </summary>
public static class DemoDocumentData
{
    /// <summary>
    /// Gets a collection of all demo documents.
    /// </summary>
    /// <value>
    /// An <see cref="IEnumerable{T}"/> of <see cref="DocumentDetails"/> containing all demo documents.
    /// </value>
    internal static IEnumerable<DocumentDetails> Data => [PJ, JB];

    /// <summary>
    /// Gets a demo document for JB (Jean Bernard).
    /// </summary>
    /// <value>
    /// A <see cref="DocumentDetails"/> object representing JB's document information.
    /// </value>
    internal static DocumentDetails JB => new(
        "JB",
        "Livres",
        "Un livre de SF",
        "Un roman de science fiction",
        false);

    /// <summary>
    /// Gets a demo document for PJ (Piquot Jérôme).
    /// </summary>
    /// <value>
    /// A <see cref="DocumentDetails"/> object representing PJ's document information.
    /// </value>
    internal static DocumentDetails PJ => new(
        "PJ",
        "Photos",
        "Photo Paris",
        "Une photo de Paris",
        false);
}