namespace Hexalith.Documents.UI.Pages.DocumentTypes;

using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing file types.
/// </summary>
public class DocumentTypeEditViewModel
{
    private string _id = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the file type.</param>
    public DocumentTypeEditViewModel(DocumentTypeDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Name = details.Name;
        Description = details.Description;
        Disabled = details.Disabled;
        FileTypeIds = [.. details.FileTypeIds];
        DataExtractionIds = [.. details.DataExtractionIds];
        Tags = [.. details.Tags];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditViewModel"/> class.
    /// </summary>
    public DocumentTypeEditViewModel()
        : this(new DocumentTypeDetailsViewModel(
        UniqueIdHelper.GenerateUniqueStringId(),
        string.Empty,
        null,
        [],
        [],
        [],
        false))
    {
    }

    public bool DataExtractionChanged => !DataExtractionIds.SequenceEqual(Original.DataExtractionIds);

    public ICollection<string> DataExtractionIds { get; } = [];

    /// <summary>
    /// Gets or sets the description of the file type.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets a value indicating whether the description has changed.
    /// </summary>
    public bool DescriptionChanged => Description != Original.Description || Name != Original.Name;

    /// <summary>
    /// Gets or sets a value indicating whether the file type is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets the targets associated with the file type.
    /// </summary>
    public ICollection<string> FileTypeIds { get; } = [];

    /// <summary>
    /// Gets a value indicating whether the targets have changed.
    /// </summary>
    public bool FileTypesChanged => !FileTypeIds.SequenceEqual(Original.FileTypeIds);

    /// <summary>
    /// Gets a value indicating whether there are changes in the file type details.
    /// </summary>
    public bool HasChanges =>
        Id != Original.Id ||
        DescriptionChanged ||
        TagsChanged ||
        DataExtractionChanged ||
        FileTypesChanged ||
        Disabled != Original.Disabled;

    /// <summary>
    /// Gets or sets the ID of the file type.
    /// </summary>
    public string Id
    {
        get => string.IsNullOrWhiteSpace(Original.Id) ? _id : Original.Id;
        set => _id = string.IsNullOrWhiteSpace(Original.Id) ? value : Original.Id;
    }

    /// <summary>
    /// Gets or sets the name of the file type.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original details of the file type.
    /// </summary>
    public DocumentTypeDetailsViewModel Original { get; }

    public ICollection<DocumentTag> Tags { get; } = [];

    public bool TagsChanged => !Tags.SequenceEqual(Original.Tags);
}