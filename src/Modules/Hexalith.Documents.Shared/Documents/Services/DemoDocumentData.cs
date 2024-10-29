namespace Hexalith.Documents.Shared.Documents.Services;

using Hexalith.Document.Domain.ValueObjects;
using Hexalith.Documents.Shared.Documents.ViewModels;

/// <summary>
/// Provides demo document data for testing and demonstration purposes.
/// </summary>
public static class DemoDocumentData
{
    /// <summary>
    /// Gets a demo document for PJ (Piquot Jérôme).
    /// </summary>
    /// <value>
    /// A <see cref="DocumentDetails"/> object representing PJ's document information.
    /// </value>
    internal static DocumentDetails PJ => new(
        "PJ",
        "Piquot Jérôme",
        "AI Technical architect",
        new Person(),
        [
            new("Mobile", DocumentPointType.Mobile, "+33651818181"),
            new("Office phone", DocumentPointType.Phone, "+33145453333"),
            new("Email", DocumentPointType.Email, "jpiquot@hexalith.com")],
        false);

    /// <summary>
    /// Gets a demo document for JB (Jean Bernard).
    /// </summary>
    /// <value>
    /// A <see cref="DocumentDetails"/> object representing JB's document information.
    /// </value>
    internal static DocumentDetails JB => new(
        "JB",
        "Jean Bernard",
        "Pediatric Doctor",
        new Person(),
        [
            new("Mobile", DocumentPointType.Mobile, "+33651717171"),
            new("Office phone", DocumentPointType.Phone, "+33145456666"),
            new("Email", DocumentPointType.Email, "jbernard@hexalith.com")],
        false);

    /// <summary>
    /// Gets a collection of all demo documents.
    /// </summary>
    /// <value>
    /// An <see cref="IEnumerable{T}"/> of <see cref="DocumentDetails"/> containing all demo documents.
    /// </value>
    internal static IEnumerable<DocumentDetails> Data => [PJ, JB];
}
