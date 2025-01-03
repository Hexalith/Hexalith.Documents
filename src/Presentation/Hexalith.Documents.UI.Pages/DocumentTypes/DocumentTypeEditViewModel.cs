namespace Hexalith.Documents.UI.Pages.DocumentTypes;

using System.Collections.ObjectModel;
using System.Security.Claims;

using Hexalith.Application.Requests;
using Hexalith.Application.Services;
using Hexalith.Documents.Domain.ValueObjects;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Documents.Requests.FileTypes;
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
    /// <param name="fileTypes">The summaries of the file types.</param>
    public DocumentTypeEditViewModel(DocumentTypeDetailsViewModel details, Collection<Option<string>> fileTypes)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Id = details.Id;
        Name = details.Name;
        Comments = details.Comments;
        Disabled = details.Disabled;
        FileTypeIds = fileTypes;
        DataExtractionIds = [.. details.DataExtractionIds];
        Tags = [.. details.Tags];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentTypeEditViewModel"/> class.
    /// </summary>
    public DocumentTypeEditViewModel()
        : this(
            new DocumentTypeDetailsViewModel(
                UniqueIdHelper.GenerateUniqueStringId(),
                string.Empty,
                null,
                [],
                [],
                [],
                false),
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

    /// <summary>
    /// Creates a new instance of <see cref="DocumentTypeEditViewModel"/> asynchronously.
    /// </summary>
    /// <param name="id">The ID of the document type.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="requestService">The service to handle the request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="DocumentTypeEditViewModel"/> instance.</returns>
    public static async Task<DocumentTypeEditViewModel?> CreateAsync(string id, ClaimsPrincipal user, IRequestService requestService, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(user?.Identity?.Name, nameof(user));
        ArgumentNullException.ThrowIfNull(requestService);

        GetDocumentTypeDetails details = await requestService
            .SubmitAsync(user, new GetDocumentTypeDetails(id), cancellationToken)
            .ConfigureAwait(false);
        if (details.Result is not null)
        {
            if (details.Result.FileTypeIds.Any())
            {
                GetFileTypeSummaries fileTypeRequest = await requestService
                        .SubmitAsync(user, new GetFileTypeSummaries(details.Result.FileTypeIds), cancellationToken)
                        .ConfigureAwait(false);
                Collection<Option<string>> fileSummaries = [..fileTypeRequest.Results.Select(p => new Option<string>
                {
                    Value = p.Id,
                    Text = p.Name,
                    Selected = true,
                    Disabled = p.Disabled,
                })];
                return new DocumentTypeEditViewModel(details.Result, fileSummaries);
            }
            else
            {
                return new DocumentTypeEditViewModel(details.Result, []);
            }
        }

        return null;
    }
}