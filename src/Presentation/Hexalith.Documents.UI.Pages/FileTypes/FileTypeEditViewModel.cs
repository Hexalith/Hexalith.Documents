namespace Hexalith.Documents.UI.Pages.FileTypes;

using System.Security.Claims;

using Hexalith.Application.Commands;
using Hexalith.Application.Services;
using Hexalith.Documents.Commands.FileTypes;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.UI.Components;

/// <summary>
/// ViewModel for editing file types.
/// </summary>
internal sealed class FileTypeEditViewModel : IIdDescription, IEntityViewModel
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
    string.Empty,
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

    /// <summary>
    /// Saves the file type details asynchronously.
    /// </summary>
    /// <param name="user">The user performing the save operation.</param>
    /// <param name="commandService">The command service to submit commands.</param>
    /// <param name="create">A value indicating whether to create a new file type.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    internal async Task SaveAsync(ClaimsPrincipal user, ICommandService commandService, bool create, CancellationToken cancellationToken)
    {
        FileTypeCommand fileTypeCommand;
        if (create)
        {
            fileTypeCommand = new AddFileType(
                        Id!,
                        Name!,
                        Comments,
                        FileToTextConverter,
                        Targets);
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
            return;
        }

        if (DescriptionChanged)
        {
            fileTypeCommand = new ChangeFileTypeDescription(
            Id!,
            Name!,
            Comments);
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
        }

        if (FileToTextConverterChanged)
        {
            fileTypeCommand = new ChangeFileTypeFileToTextConverter(
            Id!,
            FileToTextConverter);
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
        }

        if (Disabled != Original.Disabled && Disabled)
        {
            fileTypeCommand = new DisableFileType(Id);
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
        }

        if (Disabled != Original.Disabled && !Disabled)
        {
            fileTypeCommand = new EnableFileType(Id);
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
        }

        // for each target in Targets, add it if it does not exist
        foreach (string target in Targets)
        {
            if (!Original.Targets.Contains(target))
            {
                fileTypeCommand = new AddFileTypeTarget(Id, target);
                await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
            }
        }

        // for each target in Original.Targets, remove it if it does not exist
        foreach (string target in Original.Targets)
        {
            if (!Targets.Contains(target))
            {
                fileTypeCommand = new RemoveFileTypeTarget(Id, target);
                await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}