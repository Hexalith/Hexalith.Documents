namespace Hexalith.Documents.UI.Pages.FileTypes.ViewModels;

using Hexalith.Documents.UI.Components.FileTypes.ViewModels;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing document types.
/// </summary>
public class FileTypeEditViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeEditViewModel"/> class.
    /// </summary>
    /// <param name="factoryDetails">The details of the document type.</param>
    public FileTypeEditViewModel(FileTypeDetailsViewModel factoryDetails)
    {
        ArgumentNullException.ThrowIfNull(factoryDetails);
        Original = factoryDetails;
        Name = factoryDetails.Name;
        Description = factoryDetails.Description;
        Disabled = factoryDetails.Disabled;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeEditViewModel"/> class.
    /// </summary>
    public FileTypeEditViewModel()
        : this(new FileTypeDetailsViewModel(
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
    public FileTypeDetailsViewModel Original { get; }
}