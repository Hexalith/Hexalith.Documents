// <copyright file="DocumentContainerEditViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.UI.Pages.DocumentContainers;

using System.Security.Claims;

using Hexalith.Application.Commands;
using Hexalith.Application.Requests;
using Hexalith.Documents.Commands.DocumentContainers;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains.ValueObjects;
using Hexalith.UI.Components;
using Hexalith.UI.Components.Helpers;

using Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// ViewModel for editing file types.
/// </summary>
public sealed class DocumentContainerEditViewModel : IIdDescription, IEntityViewModel
{
    private string _id = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContainerEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the file type.</param>
    /// <param name="storage">The document storage summary.</param>
    public DocumentContainerEditViewModel(DocumentContainerDetailsViewModel details, DocumentStorageSummaryViewModel? storage)
    {
        ArgumentNullException.ThrowIfNull(details);
        Original = details;
        Name = details.Name;
        Path = details.Path;
        Comments = details.Comments;
        Disabled = details.Disabled;
        DocumentStorage = storage is null ? [] : [storage.ToOption(true)];
        Tags = [.. details.Tags];
    }

    /// <summary>
    /// Gets an empty file type.
    /// </summary>
    public static DocumentContainerEditViewModel Empty => new(
        new DocumentContainerDetailsViewModel(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            null,
            null,
            [],
            [],
            [],
            false),
        null);

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
    /// Gets or sets the document storage options.
    /// </summary>
    public IEnumerable<Option<string>> DocumentStorage { get; set; }

    /// <summary>
    /// Gets a value indicating whether the file to text converter has changed.
    /// </summary>
    public bool DocumentStorageChanged => DocumentStorageId != Original.DocumentStorageId;

    /// <summary>
    /// Gets the document storage ID.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the document storage ID is required but not available.</exception>
    public string DocumentStorageId => DocumentStorage.FirstOrDefault()?.Value ?? throw new InvalidOperationException("Document storage ID is required.");

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
    public ICollection<DocumentTag> Tags { get; }

    /// <summary>
    /// Gets a value indicating whether the tags have changed.
    /// </summary>
    public bool TagsChanged => (Tags.Count != Original.Tags.Count()) || Tags.Except(Original.Tags).Any();

    /// <inheritdoc/>
    string IIdDescription.Description => Name;

    /// <summary>
    /// Creates a new instance of the <see cref="DocumentContainerEditViewModel"/> class asynchronously.
    /// </summary>
    /// <param name="id">The ID of the document container.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="requestService">The request service to use.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created <see cref="DocumentContainerEditViewModel"/> instance.</returns>
    internal static async Task<DocumentContainerEditViewModel?> CreateAsync(string id, ClaimsPrincipal user, IRequestService requestService, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(user?.Identity?.Name);
        ArgumentNullException.ThrowIfNull(requestService);

        DocumentContainerDetailsViewModel details = await requestService
            .GetDocumentContainerDetailsAsync(id, user, cancellationToken)
            .ConfigureAwait(false);
        DocumentStorageSummaryViewModel storage = await requestService
            .GetDocumentStorageSummaryAsync(details.DocumentStorageId, user, cancellationToken)
            .ConfigureAwait(false);
        return new DocumentContainerEditViewModel(details, storage);
    }

    /// <summary>
    /// Saves the document container asynchronously.
    /// </summary>
    /// <param name="user">The user making the request.</param>
    /// <param name="commandService">The command service to use.</param>
    /// <param name="create">A value indicating whether to create a new document container.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    internal async Task SaveAsync(ClaimsPrincipal user, ICommandService commandService, bool create, CancellationToken cancellationToken)
    {
        if (!HasChanges)
        {
            return;
        }

        DocumentContainerCommand command;
        if (create)
        {
            command = new CreateDocumentContainer(
                        Id!,
                        DocumentStorageId,
                        Name,
                        Path,
                        Comments,
                        null);
            await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
            return;
        }

        if (DescriptionChanged)
        {
            command = new ChangeDocumentContainerDescription(
                        Id!,
                        Name!,
                        Comments);
            await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
        }

        if (Disabled != Original.Disabled && Disabled)
        {
            command = new DisableDocumentContainer(Id);
            await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
        }

        if (Disabled != Original.Disabled && !Disabled)
        {
            command = new EnableDocumentContainer(Id);
            await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
        }

        // for each tag in tags, add it if it does not exist
        foreach (DocumentTag tag in Tags)
        {
            if (!Original.Tags.Contains(tag))
            {
                command = new AddDocumentContainerTag(Id, tag.Key, tag.Value, tag.Unique);
                await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
            }
        }

        // for each tag in tags, remove it if it does not exist
        foreach (DocumentTag target in Original.Tags)
        {
            if (!Tags.Contains(target))
            {
                command = new RemoveDocumentContainerTag(Id, target.Key, target.Value);
                await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}