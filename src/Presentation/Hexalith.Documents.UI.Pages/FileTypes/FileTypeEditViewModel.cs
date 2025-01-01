namespace Hexalith.Documents.UI.Pages.FileTypes;

using Hexalith.Application.Services;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Extensions.Helpers;

/// <summary>
/// ViewModel for editing file types.
/// </summary>
public sealed class FileTypeEditViewModel : IIdDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileTypeEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the file type.</param>
    public FileTypeEditViewModel(FileTypeDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Id = details.Id;
        Original = details;
        Name = details.Name;
        Comments = details.Comments;
        Disabled = details.Disabled;
        FileToTextConverter = details.FileToTextConverter;
        Targets = [.. details.Targets];
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
    /// Gets or sets the file to text converter.
    /// </summary>
    public string? FileToTextConverter { get; set; }

    /// <summary>
    /// Gets a value indicating whether the file to text converter has changed.
    /// </summary>
    public bool FileToTextConverterChanged => FileToTextConverter != Original.FileToTextConverter;

    /// <summary>
    /// Gets a value indicating whether there are changes in the file type details.
    /// </summary>
    public bool HasChanges =>
    Id != Original.Id ||
    DescriptionChanged ||
    FileToTextConverterChanged ||
    TargetsChanged ||
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
    public FileTypeDetailsViewModel Original { get; }

    /// <summary>
    /// Gets the targets associated with the file type.
    /// </summary>
    public ICollection<string> Targets { get; } = [];

    /// <summary>
    /// Gets a value indicating whether the targets have changed.
    /// </summary>
    public bool TargetsChanged => !Targets.SequenceEqual(Original.Targets);

    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}