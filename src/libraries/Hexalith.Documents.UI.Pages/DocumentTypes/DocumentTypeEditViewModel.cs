namespace Hexalith.Documents.UI.Pages.DocumentTypes;

using System.Security.Claims;

using Hexalith.Application.Commands;
using Hexalith.Application.Requests;
using Hexalith.Documents.Commands.DocumentTypes;
using Hexalith.Documents.ValueObjects;
using Hexalith.Documents.Requests.DocumentInformationExtractions;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Domains.ValueObjects;
using Hexalith.UI.Components;
using Hexalith.UI.Components.Helpers;

using Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// ViewModel for editing file types.
/// </summary>
internal sealed class DocumentTypeEditViewModel : IIdDescription, IEntityViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the file type.</param>
    /// <param name="dataExtractionIds">The summaries of the data extraction IDs.</param>
    /// <param name="fileTypes">The summaries of the file types.</param>
    public DocumentTypeEditViewModel(
        DocumentTypeDetailsViewModel details,
        IEnumerable<Option<string>> dataExtractionIds,
        IEnumerable<Option<string>> fileTypes)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Id = details.Id;
        Name = details.Name;
        Comments = details.Comments;
        Disabled = details.Disabled;
        FileTypeIds = [.. fileTypes];
        DataExtractionIds = [.. dataExtractionIds];
        Tags = [.. details.Tags];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditViewModel"/> class.
    /// </summary>
    public DocumentTypeEditViewModel()
        : this(
            new DocumentTypeDetailsViewModel(
                string.Empty,
                string.Empty,
                null,
                [],
                [],
                [],
                false),
            [],
            [])
    {
    }

    /// <summary>
    /// Gets or sets the description of the file type.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Gets a value indicating whether the data extraction has changed.
    /// </summary>
    public bool DataExtractionChanged => !DataExtractionIds.Select(p => p.Value).SequenceEqual(Original.DataExtractionIds);

    /// <summary>
    /// Gets or sets the data extraction IDs associated with the file type.
    /// </summary>
    public IEnumerable<Option<string>> DataExtractionIds { get; set; }

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

    /// <summary>
    /// Creates a new instance of <see cref="DocumentTypeEditViewModel"/> asynchronously.
    /// </summary>
    /// <param name="id">The ID of the document type.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="requestService">The service to handle the request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="DocumentTypeEditViewModel"/> instance.</returns>
    internal static async Task<DocumentTypeEditViewModel?> CreateAsync(string id, ClaimsPrincipal user, IRequestService requestService, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(user?.Identity?.Name, nameof(user));
        ArgumentNullException.ThrowIfNull(requestService);

        GetDocumentTypeDetails details = await requestService
            .SubmitAsync(user, new GetDocumentTypeDetails(id), cancellationToken)
            .ConfigureAwait(false);
        if (details.Result is not null)
        {
            List<string> fileTypeIds = [.. details.Result.FileTypeIds];
            IEnumerable<Option<string>> fileSummaries = [];
            IEnumerable<Option<string>> extractionSummaries = [];
            if (fileTypeIds.Count > 0)
            {
                GetFileTypeSummaries fileTypeRequest = await requestService
                        .SubmitAsync(user, new GetFileTypeSummaries(fileTypeIds), cancellationToken)
                        .ConfigureAwait(false);
                fileSummaries = fileTypeRequest.Results.ToOptions(true);
            }

            List<string> extractionIds = [.. details.Result.DataExtractionIds];
            if (extractionIds.Count > 0)
            {
                GetDocumentInformationExtractionSummaries extractionRequest = await requestService
                        .SubmitAsync(user, new GetDocumentInformationExtractionSummaries(extractionIds), cancellationToken)
                        .ConfigureAwait(false);
                extractionSummaries = extractionRequest.Results.ToOptions(true);
            }

            return new DocumentTypeEditViewModel(details.Result, extractionSummaries, fileSummaries);
        }

        return null;
    }

    /// <summary>
    /// Saves the document type asynchronously.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="commandService">The service to handle the command.</param>
    /// <param name="create">A value indicating whether to create a new document type.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    internal async Task SaveAsync(ClaimsPrincipal user, ICommandService commandService, bool create, CancellationToken cancellationToken)
    {
        if (!HasChanges)
        {
            return;
        }

        DocumentTypeCommand fileTypeCommand;
        if (create)
        {
            fileTypeCommand = new AddDocumentType(
                       Id,
                       Name,
                       Comments,
                       DataExtractionIds
                            .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                            .Select(p => p.Value!),
                       FileTypeIds
                            .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                            .Select(p => p.Value!));
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
            return;
        }

        if (DescriptionChanged)
        {
            fileTypeCommand = new ChangeDocumentTypeDescription(
                        Id!,
                        Name!,
                        Comments);
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
        }

        if (Disabled != Original.Disabled && Disabled)
        {
            fileTypeCommand = new DisableDocumentType(Id);
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
        }

        if (Disabled != Original.Disabled && !Disabled)
        {
            fileTypeCommand = new EnableDocumentType(Id);
            await commandService.SubmitCommandAsync(user, fileTypeCommand, cancellationToken).ConfigureAwait(false);
        }

        await UpdateFileTypeIdsAsync(user, commandService, cancellationToken).ConfigureAwait(false);
        await UpdateExtractionIdsAsync(user, commandService, cancellationToken).ConfigureAwait(false);
    }

    private async Task UpdateExtractionIdsAsync(ClaimsPrincipal user, ICommandService commandService, CancellationToken cancellationToken)
    {
        // for each file type in ExtractionIds, ADD it if it does not exist
        foreach (string item in DataExtractionIds
            .Where(p => !string.IsNullOrWhiteSpace(p.Value))
            .Select(p => p.Value!))
        {
            if (!Original.DataExtractionIds.Contains(item))
            {
                AddDocumentTypeDataExtraction c = new(Id, item);
                await commandService.SubmitCommandAsync(user, c, cancellationToken).ConfigureAwait(false);
            }
        }

        // for each file type in original, REMOVE it if it does not exist in ExtractionIds
        foreach (string item in Original.DataExtractionIds)
        {
            if (!DataExtractionIds.Any(p => p.Value == item))
            {
                RemoveDocumentTypeDataExtraction c = new(Id, item);
                await commandService.SubmitCommandAsync(user, c, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    private async Task UpdateFileTypeIdsAsync(ClaimsPrincipal user, ICommandService commandService, CancellationToken cancellationToken)
    {
        // for each file type in FileTypeIds, ADD it if it does not exist
        foreach (string item in FileTypeIds
            .Where(p => !string.IsNullOrWhiteSpace(p.Value))
            .Select(p => p.Value!))
        {
            if (!Original.FileTypeIds.Contains(item))
            {
                AddDocumentTypeFileType c = new(Id, item);
                await commandService.SubmitCommandAsync(user, c, cancellationToken).ConfigureAwait(false);
            }
        }

        // for each file type in original, REMOVE it if it does not exist in FileTypeIds
        foreach (string item in Original.FileTypeIds)
        {
            if (!FileTypeIds.Any(p => p.Value == item))
            {
                RemoveDocumentTypeFileType c = new(Id, item);
                await commandService.SubmitCommandAsync(user, c, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}