namespace Hexalith.Documents.UI.Services.Documents.ViewModels;

/// <summary>
/// Represents detailed information about a document.
/// </summary>
/// <param name="Id">The unique identifier of the document.</param>
/// <param name="DocumentTypeId">The unique identifier of the document type.</param>
/// <param name="Name">The name or title of the document.</param>
/// <param name="Description">A detailed description of the document.</param>
/// <param name="Disabled">Indicates whether the document is disabled or not.</param>
public record DocumentDetailsViewModel(string Id, string DocumentTypeId, string Name, string Description, bool Disabled);