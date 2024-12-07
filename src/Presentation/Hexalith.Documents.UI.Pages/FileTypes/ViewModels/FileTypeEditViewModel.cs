namespace Hexalith.Documents.UI.Pages.FileTypes.ViewModels;

using System.Collections.Immutable;

using Hexalith.Documents.UI.Services.FileTypes.ViewModels;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing file types.
/// </summary>
public class FileTypeEditViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the file type.</param>
    public FileTypeEditViewModel(FileTypeDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Name = details.Name;
        Description = details.Description;
        Disabled = details.Disabled;
        Targets = details.Targets.ToImmutableList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeEditViewModel"/> class.
    /// </summary>
    public FileTypeEditViewModel()
        : this(new FileTypeDetailsViewModel(
        UniqueIdHelper.GenerateUniqueStringId(),
        string.Empty,
        null,
        null,
        [],
        false))
    {
    }

    /// <summary>
    /// Gets or sets the description of the file type.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the file type is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets a value indicating whether there are changes in the file type details.
    /// </summary>
    public bool HasChanges => Name != Original.Name || Description != Original.Description || Disabled != Original.Disabled;

    /// <summary>
    /// Gets the ID of the file type.
    /// </summary>
    public string Id => Original.Id;

    /// <summary>
    /// Gets or sets the name of the file type.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original details of the file type.
    /// </summary>
    public FileTypeDetailsViewModel Original { get; }

    /// <summary>
    /// Gets the targets associated with the file type.
    /// </summary>
    public IEnumerable<string> Targets { get; }
}