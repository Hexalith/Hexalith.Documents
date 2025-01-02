namespace Hexalith.Documents.UI.Pages.DocumentTypes;

using Hexalith.Application.Services;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Extensions.Helpers;

using Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// ViewModel for editing file types.
/// </summary>
public sealed class DocumentTypeEditViewModel : IIdDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the file type.</param>
    public DocumentTypeEditViewModel(DocumentTypeDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Id = details.Id;
        Name = details.Name;
        Comments = details.Comments;
        Disabled = details.Disabled;
        FileTypeIds = [.. details.FileTypeIds.Select(p => new Option<string?>() { Value = p, Text = p })];
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

    /// <summary>
    /// Gets or sets the description of the file type.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Gets a value indicating whether the data extraction has changed.
    /// </summary>
    public bool DataExtractionChanged => !DataExtractionIds.SequenceEqual(Original.DataExtractionIds);

    /// <summary>
    /// Gets the data extraction IDs associated with the file type.
    /// </summary>
    public ICollection<string> DataExtractionIds { get; } = [];

    /// <summary>
    /// Gets a value indicating whether the description has changed.
    /// </summary>
    public bool DescriptionChanged => Comments != Original.Comments || Name != Original.Name;

    /// <summary>
    /// Gets or sets a value indicating whether the file type is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the targets associated with the file type.
    /// </summary>
    public IEnumerable<Option<string>> FileTypeIds { get; set; }

    /// <summary>
    /// Gets a value indicating whether the targets have changed.
    /// </summary>
    public bool FileTypesChanged => !FileTypeIds.Select(p => p.Value).SequenceEqual(Original.FileTypeIds);

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
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the file type.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original details of the file type.
    /// </summary>
    public DocumentTypeDetailsViewModel Original { get; }

    /// <summary>
    /// Gets the tags associated with the file type.
    /// </summary>
    public ICollection<DocumentTag> Tags { get; } = [];

    /// <summary>
    /// Gets a value indicating whether the tags have changed.
    /// </summary>
    public bool TagsChanged => !Tags.SequenceEqual(Original.Tags);

    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}