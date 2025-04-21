// <copyright file="DataManagementEditViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.UI.Pages.DataManagements;

using Hexalith.Documents.Requests.DataManagements;
using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents a view model for editing data management details.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataManagementEditViewModel"/> class with the specified original details.
/// </remarks>
/// <param name="original">The original details of the data management item.</param>
public sealed class DataManagementEditViewModel(DataManagementDetailsViewModel original) : IIdDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataManagementEditViewModel"/> class.
    /// </summary>
    public DataManagementEditViewModel()
        : this(new DataManagementDetailsViewModel(
            string.Empty,
            0L,
            null,
            DateTimeOffset.MinValue,
            null))
    {
    }

    /// <summary>
    /// Gets or sets the comments of the data management item.
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Gets a value indicating whether the comments have changed from the original.
    /// </summary>
    public bool CommentsChanged => Comments != Original.Comments;

    /// <summary>
    /// Gets the completion date and time of the data management item.
    /// </summary>
    public DateTimeOffset? CompletedAt => Original.CompletedAt;

    /// <summary>
    /// Gets a value indicating whether there are any changes in the data management details.
    /// </summary>
    public bool HasChanges => CommentsChanged;

    /// <summary>
    /// Gets the unique identifier of the data management item.
    /// </summary>
    public string Id => Original.Id;

    /// <summary>
    /// Gets the original details of the data management item.
    /// </summary>
    public DataManagementDetailsViewModel Original { get; } = original;

    /// <summary>
    /// Gets the size of the data management item.
    /// </summary>
    public long Size => Original.Size;

    /// <summary>
    /// Gets the start date and time of the data management item.
    /// </summary>
    public DateTimeOffset StartedAt => Original.StartedAt;

    /// <inheritdoc/>
    string IIdDescription.Description => Original.Id;

    /// <inheritdoc/>
    bool IIdDescription.Disabled => Original.CompletedAt is null;
}