namespace Hexalith.Documents.UI.Pages.DocumentTypes.ViewModels;

using Hexalith.Documents.UI.Components.DocumentTypes.ViewModels;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing document types.
/// </summary>
public class DocumentTypeEditViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditViewModel"/> class.
    /// </summary>
    /// <param name="factoryDetails">The details of the document type.</param>
    public DocumentTypeEditViewModel(DocumentTypeDetails factoryDetails)
    {
        ArgumentNullException.ThrowIfNull(factoryDetails);
        Original = factoryDetails;
        Name = factoryDetails.Name;
        Description = factoryDetails.Description;
        Disabled = factoryDetails.Disabled;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditViewModel"/> class.
    /// </summary>
    public DocumentTypeEditViewModel()
        : this(new DocumentTypeDetails(
        UniqueIdHelper.GenerateUniqueStringId(),
        string.Empty,
        string.Empty,
        false))
    {
    }

    /// <summary>
    /// Gets or sets the description of the document type.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the document type is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets a value indicating whether there are changes in the document type details.
    /// </summary>
    public bool HasChanges => Name != Original.Name || Description != Original.Description || Disabled != Original.Disabled;

    /// <summary>
    /// Gets the ID of the document type.
    /// </summary>
    public string Id => Original.Id;

    /// <summary>
    /// Gets or sets the name of the document type.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original details of the document type.
    /// </summary>
    public DocumentTypeDetails Original { get; }
}