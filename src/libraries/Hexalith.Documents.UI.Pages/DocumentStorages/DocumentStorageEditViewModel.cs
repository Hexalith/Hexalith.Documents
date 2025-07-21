// <copyright file="DocumentStorageEditViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.UI.Pages.DocumentStorages;

using System.Security.Claims;

using Hexalith.Application.Commands;
using Hexalith.Application.Requests;
using Hexalith.Documents.Commands.DocumentStorages;
using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains.ValueObjects;
using Hexalith.UI.Components;

/// <summary>
/// Represents a view model for editing document storage details.
/// </summary>
/// <seealso cref="IIdDescription"/>
public sealed class DocumentStorageEditViewModel : IEntityViewModel, IIdDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentStorageEditViewModel"/> class.
    /// </summary>
    /// <param name="details">The original document storage details.</param>
    /// <exception cref="ArgumentNullException">Thrown when details is null.</exception>
    public DocumentStorageEditViewModel(DocumentStorageDetailsViewModel details)
    {
        ArgumentNullException.ThrowIfNull(details);
        Id = details.Id;
        Original = details;
        Name = details.Name;
        ConnectionString = details.ConnectionString;
        Comments = details.Comments;
        Disabled = details.Disabled;
        StorageType = details.StorageType.ToString();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentStorageEditViewModel"/> class with default values.
    /// </summary>
    public DocumentStorageEditViewModel()
        : this(new DocumentStorageDetailsViewModel(
        string.Empty,
        string.Empty,
        DocumentStorageType.FileSystem,
        string.Empty,
        null,
        false))
    {
    }

    /// <summary>
    /// Gets or sets optional comments about the document storage.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Gets or sets the connection string for the document storage.
    /// </summary>
    public string? ConnectionString { get; set; }

    /// <summary>
    /// Gets a value indicating whether the description (Comments or Name) has been modified from the original.
    /// </summary>
    public bool DescriptionChanged => Comments != Original.Comments || Name != Original.Name;

    /// <summary>
    /// Gets or sets a value indicating whether the document storage is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets a value indicating whether any properties have been modified from their original values.
    /// </summary>
    public bool HasChanges =>
        Id != Original.Id ||
        DescriptionChanged ||
        Disabled != Original.Disabled ||
        StorageTypeChanged;

    /// <summary>
    /// Gets or sets the unique identifier for the document storage.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the document storage.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the original document storage details for tracking changes.
    /// </summary>
    public DocumentStorageDetailsViewModel Original { get; }

    /// <summary>
    /// Gets or sets the type of document storage.
    /// </summary>
    public string StorageType { get; set; }

    /// <summary>
    /// Gets a value indicating whether the storage type or connection string has been modified from the original.
    /// </summary>
    public bool StorageTypeChanged => StorageType != Original.StorageType.ToString() || Original.ConnectionString != ConnectionString;

    /// <inheritdoc/>
    string IIdDescription.Description => Name;

    /// <summary>
    /// Creates a new instance of the <see cref="DocumentStorageEditViewModel"/> class asynchronously.
    /// </summary>
    /// <param name="id">The ID of the document storage.</param>
    /// <param name="user">The user making the request.</param>
    /// <param name="requestService">The request service.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="DocumentStorageEditViewModel"/> instance.</returns>
    internal static async Task<DocumentStorageEditViewModel?> CreateAsync(string id, ClaimsPrincipal user, IRequestService requestService, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(user?.Identity?.Name);
        ArgumentNullException.ThrowIfNull(requestService);

        GetDocumentStorageDetails details = await requestService
            .SubmitAsync(user, new GetDocumentStorageDetails(id), cancellationToken)
            .ConfigureAwait(false);
        return details.Result is not null ? new DocumentStorageEditViewModel(details.Result) : null;
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

        DocumentStorageCommand command;
        if (create)
        {
            command = new AddDocumentStorage(
                       Id,
                       Name,
                       Enum.Parse<DocumentStorageType>(StorageType),
                       Comments,
                       ConnectionString);
            await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
            return;
        }

        if (!Disabled)
        {
            if (Disabled != Original.Disabled)
            {
                command = new EnableDocumentStorage(Id);
                await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
            }

            if (DescriptionChanged)
            {
                command = new ChangeDocumentStorageDescription(
                            Id!,
                            Name!,
                            Comments);
                await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
            }

            if (StorageTypeChanged)
            {
                command = new ChangeDocumentStorageType(Id, Enum.Parse<DocumentStorageType>(StorageType), ConnectionString);
                await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
            }
        }

        if (Disabled != Original.Disabled && Disabled)
        {
            command = new DisableDocumentStorage(Id);
            await commandService.SubmitCommandAsync(user, command, cancellationToken).ConfigureAwait(false);
        }
    }
}