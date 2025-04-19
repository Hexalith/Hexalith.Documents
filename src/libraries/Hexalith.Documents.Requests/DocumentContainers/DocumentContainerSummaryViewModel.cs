// <copyright file="DocumentContainerSummaryViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.DocumentContainers;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents a summary view model for a document container.
/// </summary>
/// <param name="Id">The unique identifier of the document container.</param>
/// <param name="DocumentStorageId">The identifier of the document storage this container belongs to.</param>
/// <param name="Name">The name of the document container.</param>
/// <param name="Disabled">Indicates whether the document container is disabled.</param>
[DataContract]
public sealed partial record DocumentContainerSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string DocumentStorageId,
    [property: DataMember(Order = 3)] string Name,
    [property: DataMember(Order = 4)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}