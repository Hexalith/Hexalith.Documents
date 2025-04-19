// <copyright file="DocumentEditViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.UI.Pages.Documents;

using System.Collections.Generic;
using System.Security.Claims;

using Hexalith.Application.Requests;
using Hexalith.Documents.Documents;
using Hexalith.Documents.Requests.DocumentContainers;
using Hexalith.Documents.Requests.Documents;
using Hexalith.Documents.Requests.DocumentTypes;
using Hexalith.Documents.Requests.FileTypes;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains.ValueObjects;
using Hexalith.Extensions.Helpers;
using Hexalith.UI.Components.Helpers;

using Microsoft.FluentUI.AspNetCore.Components;

/// <summary>
/// ViewModel for editing document information extraction details.
/// </summary>
public sealed class DocumentEditViewModel : IIdDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The details of the document information extraction.</param>
    /// <param name="container">The document container.</param>
    /// <param name="parent">The parent document.</param>
    /// <param name="documentType">The document type.</param>
    /// <param name="fileTypes">The file types.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentEditViewModel(
        DocumentDetailsViewModel details,
        DocumentContainerDetailsViewModel? container,
        DocumentSummaryViewModel? parent,
        DocumentTypeDetailsViewModel? documentType,
        IEnumerable<FileTypeSummaryViewModel> fileTypes)
    {
        ArgumentNullException.ThrowIfNull(details);
        Id = details.Id;
        Name = details.Description.Name;
        Comments = details.Description.Comments;
        DocumentType = documentType is null ? [] : [documentType.ToOption(true)];
        DocumentContainer = container is null ? [] : [container.ToOption(true)];
        ParentDocument = parent is null ? [] : [parent.ToOption(true)];
        Summary = details.Description.Summary;
        FromContactId = details.Routing?.FromContactId;
        ToContactIds = details.Routing?.ToContactIds;
        CopyToContactIds = details.Routing?.CopyToContactIds;
        Actors = details.Actors;
        Tags = details.Tags;
        Disabled = details.Disabled;
        Original = details;
        SelectedFileTypes = [.. fileTypes];
        Files = details.Files;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentEditViewModel"/> class.
    /// </summary>
    public DocumentEditViewModel()
        : this(
            new DocumentDetailsViewModel(
                UniqueIdHelper.GenerateUniqueStringId(),
                new DocumentDescription(string.Empty, null, null, null, null),
                null,
                string.Empty,
                DocumentState.Create(DateTimeOffset.MinValue, string.Empty),
                [],
                [],
                [],
                false),
            null,
            null,
            null,
            [])
    {
    }

    /// <summary>
    /// Gets or sets the document actors.
    /// </summary>
    public IEnumerable<DocumentActor> Actors { get; set; }

    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Gets or sets the copy to contact IDs.
    /// </summary>
    public IEnumerable<string>? CopyToContactIds { get; set; }

    /// <summary>
    /// Gets a value indicating whether the description has changed.
    /// </summary>
    public bool DescriptionChanged => Comments != Original.Description.Comments ||
        Name != Original.Description.Name ||
        DocumentTypeId != Original.Description.DocumentTypeId ||
        DocumentContainerId != Original.Description.DocumentContainerId ||
        Summary != Original.Description.Summary;

    /// <summary>
    /// Gets or sets a value indicating whether the item is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the document container.
    /// </summary>
    public IEnumerable<Option<string>> DocumentContainer { get; set; }

    /// <summary>
    /// Gets the document container ID.
    /// </summary>
    public string? DocumentContainerId => DocumentContainer.FirstOrDefault()?.Value;

    /// <summary>
    /// Gets or sets the document type.
    /// </summary>
    public IEnumerable<Option<string>> DocumentType { get; set; }

    /// <summary>
    /// Gets the document type ID.
    /// </summary>
    public string? DocumentTypeId => DocumentType.FirstOrDefault()?.Value;

    /// <summary>
    /// Gets the file content types.
    /// </summary>
    public string FileContentTypes => string.Join(", ", SelectedFileTypes.Select(p => p.ContentType));

    /// <summary>
    /// Gets or sets the file description.
    /// </summary>
    public IEnumerable<FileDescription> Files { get; set; }

    /// <summary>
    /// Gets or sets the from contact ID.
    /// </summary>
    public string? FromContactId { get; set; }

    /// <summary>
    /// Gets a value indicating whether there are any changes.
    /// </summary>
    public bool HasChanges =>
        Id != Original.Id ||
        DescriptionChanged ||
        RoutingChanged ||
        ParentDocumentId != Original.ParentDocumentId ||
        Disabled != Original.Disabled;

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original details.
    /// </summary>
    public DocumentDetailsViewModel Original { get; }

    /// <summary>
    /// Gets or sets the parent document.
    /// </summary>
    public IEnumerable<Option<string>> ParentDocument { get; set; }

    /// <summary>
    /// Gets the parent document ID.
    /// </summary>
    public string? ParentDocumentId => ParentDocument.FirstOrDefault()?.Value;

    /// <summary>
    /// Gets a value indicating whether the routing has changed.
    /// </summary>
    public bool RoutingChanged => FromContactId != Original.Routing?.FromContactId ||
        ToContactIds != Original.Routing?.ToContactIds ||
        CopyToContactIds != Original.Routing?.CopyToContactIds;

    /// <summary>
    /// Gets the selected document container.
    /// </summary>
    public DocumentContainerDetailsViewModel? SelectedDocumentContainer { get; private set; }

    /// <summary>
    /// Gets the selected document type.
    /// </summary>
    public DocumentTypeDetailsViewModel? SelectedDocumentType { get; private set; }

    /// <summary>
    /// Gets the selected file types.
    /// </summary>
    public IEnumerable<FileTypeSummaryViewModel> SelectedFileTypes { get; private set; }

    /// <summary>
    /// Gets the document state.
    /// </summary>
    public DocumentState State => Original.State;

    /// <summary>
    /// Gets or sets the summary.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the document tags.
    /// </summary>
    public IEnumerable<DocumentTag> Tags { get; set; }

    /// <summary>
    /// Gets or sets the to contact IDs.
    /// </summary>
    public IEnumerable<string>? ToContactIds { get; set; }

    /// <inheritdoc/>
    string IIdDescription.Description => Name;

    /// <summary>
    /// Creates a new instance of the <see cref="DocumentEditViewModel"/> class asynchronously.
    /// </summary>
    /// <param name="id">The document ID to load, or null to create a new document.</param>
    /// <param name="containerId">The container ID for new documents.</param>
    /// <param name="user">The current user's claims principal.</param>
    /// <param name="requestService">The service used to submit requests.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the document edit view model.</returns>
    /// <exception cref="ArgumentNullException">Thrown when user or requestService is null.</exception>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    public static async Task<DocumentEditViewModel> CreateAsync(string? id, string? containerId, ClaimsPrincipal user, IRequestService requestService, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(requestService);
        cancellationToken.ThrowIfCancellationRequested();

        DocumentDetailsViewModel details;

        details = !string.IsNullOrWhiteSpace(id) ? await requestService.GetDocumentDetailsAsync(id, user, cancellationToken).ConfigureAwait(false) : DocumentDetailsViewModel.Create(id, containerId);

        Task<DocumentContainerDetailsViewModel?> containerTask = requestService
            .FindDocumentContainerDetailsAsync(details.Description.DocumentContainerId, user, cancellationToken);
        DocumentTypeDetailsViewModel? documentType = await requestService
            .FindDocumentTypeDetailsAsync(details.Description.DocumentTypeId, user, cancellationToken).ConfigureAwait(false);

        Task<DocumentSummaryViewModel?> parentTask = requestService
            .FindDocumentSummaryAsync(details.ParentDocumentId, user, cancellationToken);

        Task<GetFileTypeSummaries> fileTypes = requestService
            .SubmitAsync(
                user,
                new GetFileTypeSummaries(documentType?.FileTypeIds ?? []),
                cancellationToken);

        return new DocumentEditViewModel(
            details,
            await containerTask.ConfigureAwait(false),
            await parentTask.ConfigureAwait(false),
            documentType,
            (await fileTypes.ConfigureAwait(false)).Results);
    }

    /// <summary>
    /// Selects a document container asynchronously.
    /// </summary>
    /// <param name="containerId">The container ID to select.</param>
    /// <param name="user">The current user's claims principal.</param>
    /// <param name="requestService">The service used to submit requests.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SelectDocumentContainerAsync(string? containerId, ClaimsPrincipal user, IRequestService requestService, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(requestService);
        cancellationToken.ThrowIfCancellationRequested();
        DocumentContainerDetailsViewModel? container = await requestService
            .FindDocumentContainerDetailsAsync(containerId, user, cancellationToken).ConfigureAwait(false);
        if (container is null)
        {
            SelectedDocumentContainer = null;
            SelectedDocumentType = null;
            DocumentContainer = [];
            DocumentType = [];
            return;
        }

        SelectedDocumentContainer = container;
        DocumentContainer = [container.ToOption(true)];
        if (container.Id != DocumentContainerId)
        {
            DocumentContainer = [container.ToOption(true)];
            DocumentType = [];
        }
    }

    /// <summary>
    /// Selects a document type asynchronously.
    /// </summary>
    /// <param name="typeId">The type ID to select.</param>
    /// <param name="user">The current user's claims principal.</param>
    /// <param name="requestService">The service used to submit requests.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SelectDocumentTypeAsync(string? typeId, ClaimsPrincipal user, IRequestService requestService, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(requestService);
        cancellationToken.ThrowIfCancellationRequested();
        DocumentTypeDetailsViewModel? type = await requestService
            .FindDocumentTypeDetailsAsync(typeId, user, cancellationToken).ConfigureAwait(false);
        if (type is null)
        {
            SelectedDocumentType = null;
            DocumentType = [];
            return;
        }

        if (type.Id != DocumentTypeId)
        {
            SelectedFileTypes = (await requestService
                .SubmitAsync(
                    user,
                    new GetFileTypeSummaries(type.FileTypeIds),
                    cancellationToken)
                .ConfigureAwait(false))
                .Results;
            SelectedDocumentType = type;
            DocumentType = [type.ToOption(true)];
        }
    }
}