namespace Hexalith.Documents.UI.Pages.Documents.ViewModels;

using Hexalith.Documents.UI.Components.Documents.ViewModels;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing a document.
/// </summary>
public class DocumentEditViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the document to be edited.</param>
    public DocumentEditViewModel(DocumentDetails details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Name = details.Name;
        Description = details.Description;
        Disabled = details.Disabled;
        DocumentTypeId = details.DocumentTypeId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditViewModel"/> class.
    /// </summary>
    public DocumentEditViewModel()
        : this(new DocumentDetails(
        UniqueIdHelper.GenerateUniqueStringId(),
        string.Empty,
        string.Empty,
        string.Empty,
        false))
    {
    }

    /// <summary>
    /// Gets or sets the description of the document.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the document is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the document type identifier.
    /// </summary>
    public string DocumentTypeId { get; set; }

    /// <summary>
    /// Gets a value indicating whether the document has changes.
    /// </summary>
    public bool HasChanges => Name != Original.Name || Description != Original.Description || Disabled != Original.Disabled || DocumentTypeId != Original.DocumentTypeId;

    /// <summary>
    /// Gets the identifier of the document.
    /// </summary>
    public string Id => Original.Id;

    /// <summary>
    /// Gets or sets the name of the document.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original details of the document.
    /// </summary>
    public DocumentDetails Original { get; }
}