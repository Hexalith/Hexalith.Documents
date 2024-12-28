namespace Hexalith.Documents.UI.Pages.DocumentContainers;

using Hexalith.Application.Services;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing file types.
/// </summary>
public sealed class DocumentContainerEditViewModel : IIdDescription
{
    private string _id = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContainerEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the file type.</param>
    public DocumentContainerEditViewModel(DocumentContainerDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Name = details.Name;
        Path = details.Path;
        Comments = details.Comments;
        Disabled = details.Disabled;
        DocumentStorageId = details.DocumentStorageId;
        Tags = [.. details.Tags];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContainerEditViewModel"/> class.
    /// </summary>
    public DocumentContainerEditViewModel()
        : this(new DocumentContainerDetailsViewModel(
        UniqueIdHelper.GenerateUniqueStringId(),
        string.Empty,
        string.Empty,
        string.Empty,
        null,
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
    /// Gets a value indicating whether the description has changed.
    /// </summary>
    public bool DescriptionChanged => Comments != Original.Comments || Name != Original.Name;

    /// <summary>
    /// Gets or sets a value indicating whether the file type is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets a value indicating whether the file to text converter has changed.
    /// </summary>
    public bool DocumentStorageChanged => DocumentStorageId != Original.DocumentStorageId;

    /// <summary>
    /// Gets or sets the file to text converter.
    /// </summary>
    public string DocumentStorageId { get; set; }

    /// <summary>
    /// Gets a value indicating whether there are changes in the file type details.
    /// </summary>
    public bool HasChanges =>
        Id != Original.Id ||
        DescriptionChanged ||
        DocumentStorageChanged ||
        TagsChanged ||
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
    public DocumentContainerDetailsViewModel Original { get; }

    /// <summary>
    /// Gets or sets the path of the file type.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Gets the targets associated with the file type.
    /// </summary>
    public ICollection<DocumentTag> Tags { get; } = [];

    /// <summary>
    /// Gets a value indicating whether the tags have changed.
    /// </summary>
    public bool TagsChanged => (Tags.Count != Original.Tags.Count()) || Tags.Except(Original.Tags).Any();

    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}