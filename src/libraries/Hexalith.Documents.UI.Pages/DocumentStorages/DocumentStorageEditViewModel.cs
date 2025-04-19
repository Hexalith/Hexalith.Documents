// <copyright file="DocumentStorageEditViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.UI.Pages.DocumentStorages;

using Hexalith.Documents.Requests.DocumentStorages;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents a view model for editing document storage details.
/// </summary>
/// <seealso cref="IIdDescription"/>
public sealed class DocumentStorageEditViewModel : IIdDescription
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
        Disabled != Original.Disabled;

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

    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}