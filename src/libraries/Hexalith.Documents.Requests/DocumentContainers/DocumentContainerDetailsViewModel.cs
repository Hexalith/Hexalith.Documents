// <copyright file="DocumentContainerDetailsViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Collections.Generic;
using System.Runtime.Serialization;

using Hexalith.Documents.ValueObjects;
using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents the details of a document container.
/// </summary>
[DataContract]
public sealed record DocumentContainerDetailsViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string DocumentStorageId,
    [property: DataMember(Order = 3)] string Name,
    [property: DataMember(Order = 4)] string Path,
    [property: DataMember(Order = 5)] string? Comments,
    [property: DataMember(Order = 6)] string? AutomaticRoutingInstructions,
    [property: DataMember(Order = 7)] IEnumerable<DocumentActor> Actors,
    [property: DataMember(Order = 8)] IEnumerable<string> DocumentTypeIds,
    [property: DataMember(Order = 9)] IEnumerable<DocumentTag> Tags,
    [property: DataMember(Order = 10)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}