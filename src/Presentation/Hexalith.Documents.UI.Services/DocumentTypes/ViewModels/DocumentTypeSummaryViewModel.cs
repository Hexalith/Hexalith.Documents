namespace Hexalith.Documents.UI.Services.DocumentTypes.ViewModels;

/// <summary>
/// Represents a summary view of a document type with essential information.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Name">The name of the document type.</param>
/// <param name="Disabled">Indicates whether the document type is disabled.</param>
public record DocumentTypeSummaryViewModel(string Id, string Name, bool Disabled)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeSummaryViewModel"/> class from a <see cref="DocumentTypeDetailsViewModel"/> object.
    /// </summary>
    /// <param name="details">The document type details to create the summary from.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentTypeSummaryViewModel(DocumentTypeDetailsViewModel details)
        : this(
              (details ?? throw new ArgumentNullException(nameof(details))).Id,
              details.Name,
              details.Disabled)
    {
    }
}