namespace Hexalith.Documents.UI.Services.Documents.ViewModels;

/// <summary>
/// Represents a summary view of a document with basic information.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Name">The name of the document.</param>
/// <param name="Disabled">A flag indicating whether the document is disabled.</param>
public record DocumentSummaryViewModel(string Id, string Name, bool Disabled)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentSummaryViewModel"/> class.
    /// </summary>
    /// <param name="details">The document details to create the summary from.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentSummaryViewModel(DocumentDetailsViewModel details)
        : this(
              (details ?? throw new ArgumentNullException(nameof(details))).Id,
              details.Name,
              details.Disabled)
    {
    }
}