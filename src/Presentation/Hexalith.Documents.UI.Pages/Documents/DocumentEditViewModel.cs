namespace Hexalith.Documents.UI.Pages.Documents;

using Hexalith.Documents.Domain.Documents;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.Documents;
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
    public DocumentEditViewModel(DocumentDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Name = details.Description.Name;
        Description = details.Description.Description;
        Disabled = details.Disabled;
        DocumentTypeId = details.Description.DocumentTypeId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditViewModel"/> class.
    /// </summary>
    public DocumentEditViewModel()
        : this(new DocumentDetailsViewModel(
        UniqueIdHelper.GenerateUniqueStringId(),
        new DocumentDescription(string.Empty, string.Empty, null, null, null),
        null,
        null,
        new DocumentState(DateTimeOffset.MinValue, string.Empty),
        [],
        new FileDescription(string.Empty, string.Empty, string.Empty, 0, string.Empty),
        [],
        false))
    {
    }

    /// <summary>
    /// Gets or sets the description of the document.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets a value indicating whether the description of the document has changed.
    /// </summary>
    public bool DescriptionChanged => Name != Original.Description.Name || Description != Original.Description.Description;

    /// <summary>
    /// Gets or sets a value indicating whether the document is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the document type identifier.
    /// </summary>
    public string? DocumentTypeId { get; set; }

    /// <summary>
    /// Gets a value indicating whether the document has changes.
    /// </summary>
    public bool HasChanges => DescriptionChanged || Disabled != Original.Disabled || DocumentTypeId != Original.Description.DocumentTypeId;

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
    public DocumentDetailsViewModel Original { get; }
}