﻿// <copyright file="DocumentSummaryViewModel.cs" company="ITANEO">
// Copyright (c) ITANEO (https://www.itaneo.com). All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Hexalith.Documents.Requests.Documents;

using System.Runtime.Serialization;

using Hexalith.Domains.ValueObjects;

/// <summary>
/// Represents a summary view of a document type with essential information.
/// </summary>
/// <param name="Id">The unique identifier of the document type.</param>
/// <param name="Name">The name of the document type.</param>
/// <param name="DocumentContainerId"></param>
/// <param name="Size"></param>
/// <param name="Disabled">Indicates whether the document type is disabled.</param>
[DataContract]
public sealed record DocumentSummaryViewModel(
    [property: DataMember(Order = 1)] string Id,
    [property: DataMember(Order = 2)] string Name,
    [property: DataMember(Order = 2)] string DocumentContainerId,
    [property: DataMember(Order = 3)] long Size,
    [property: DataMember(Order = 4)] bool Disabled) : IIdDescription
{
    /// <inheritdoc/>
    string IIdDescription.Description => Name;
}