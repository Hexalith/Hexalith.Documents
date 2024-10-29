namespace Hexalith.Documents.Shared.Documents.ViewModels;

using Hexalith.Document.Domain.Helpers;

/// <summary>
/// Represents a summary of document information.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="Name">The name of the document.</param>
/// <param name="Phone">The phone number of the document.</param>
/// <param name="Mobile">The mobile number of the document.</param>
/// <param name="Email">The email address of the document.</param>
/// <param name="Disabled">A flag indicating whether the document is disabled.</param>
public record DocumentSummary(
    string Id,
    string Name,
    string? Phone,
    string? Mobile,
    string? Email,
    bool Disabled)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentSummary"/> class from DocumentDetails.
    /// </summary>
    /// <param name="details">The DocumentDetails object to create the summary from.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentSummary(DocumentDetails details)
        : this(
              (details ?? throw new ArgumentNullException(nameof(details))).Id,
              details.Name,
              details.DocumentPoints.GetPhone(),
              details.DocumentPoints.GetMobile(),
              details.DocumentPoints.GetEmail(),
              details.Disabled)
    {
    }
}
