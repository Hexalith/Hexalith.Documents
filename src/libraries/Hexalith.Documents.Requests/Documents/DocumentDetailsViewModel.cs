// <copyright file="DocumentDetailsViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.Documents;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Documents.Documents;
using Hexalith.Documents.ValueObjects;
using Hexalith.Domains.ValueObjects;
using Hexalith.Extensions.Helpers;

/// <summary>
/// Represents the view model for document details.
/// </summary>
[DataContract]
public sealed record DocumentDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] DocumentDescription Description,
    [property: DataMember(Order = 3)] DocumentRouting? Routing,
    [property: DataMember(Order = 4)] string? ParentDocumentId,
    [property: DataMember(Order = 5)] DocumentState State,
    [property: DataMember(Order = 6)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 7)] IEnumerable<FileDescription> Files,
    [property: DataMember(Order = 8)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 9)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Description.Name;

    /// <summary>
    /// Gets an empty instance of the <see cref="DocumentDetailsViewModel"/> class.
    /// </summary>
    public static DocumentDetailsViewModel Empty => new(
        string.Empty,
        DocumentDescription.Empty,
        null,
        null,
        DocumentState.Create(System.DateTimeOffset.UtcNow, string.Empty),
        [],
        [],
        [],
        false);

    /// <summary>
    /// Creates a new instance of the <see cref="DocumentDetailsViewModel"/> class.
    /// </summary>
    /// <param name="id">The document ID.</param>
    /// <param name="documentContainerId">The document container ID.</param>
    /// <returns>A new instance of the <see cref="DocumentDetailsViewModel"/> class.</returns>
    public static DocumentDetailsViewModel Create(string? id, string? documentContainerId) => new(
        string.IsNullOrWhiteSpace(id) ? UniqueIdHelper.GenerateUniqueStringId() : id,
        new DocumentDescription(string.Empty, null, documentContainerId, null, null),
        null,
        null,
        DocumentState.Create(System.DateTimeOffset.UtcNow, string.Empty),
        [],
        [],
        [],
        false);
}